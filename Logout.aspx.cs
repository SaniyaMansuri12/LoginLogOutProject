using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace LoginLogoutPanelProject
{
    public partial class LogOut : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString);
        SqlCommand cmd = new SqlCommand();
        protected void Page_Load(object sender, EventArgs e)
        {
            Login = Label1.Text = DateTime.Now.ToString("hh:mm:ss tt");
        }  

        public string Login { get; private set; }

        public int EmpId { get; private set; }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (LogOutValidation())
            {
                Update();
            }
        }
        
        public void Update()
        {     
            cmd.Connection = con;
            cmd.CommandText = "Update TblLogin set LogOut =  GetDate() , " +
                "LogOutRemarks = '" + txtRemarkOut.Text + "' " + 
                "where Convert(Varchar, LogIn,112) = Convert(Varchar, getdate(), 112) and EmpId = " + Session["RowID"] ;
            
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            Response.Write("You are Logged Out Successfully");    
        }
        int DefaultLateInOutMin = 10;
        Boolean LogOutValidation()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand();
            // validattion for already login
            //Begin
            cmd.CommandText = "SELECT * ,Convert(Varchar,LogOut,112), Convert(Varchar,getdate(),112) " +
                                "FROM TblLogin " +
                                "WHERE EmpId = @EmpId AND Convert(Varchar, LogOut,112) = Convert(Varchar, getdate(), 112)";

            cmd.Parameters.AddWithValue("@EmpId", Session["RowID"]);

            cmd.Connection = con;
            SqlDataReader rd = cmd.ExecuteReader();
            if (rd.HasRows)
            {
                Label2.Visible = true;
                Label2.Text = "Already LogOut Please Check!!!";
                Label2.ForeColor = System.Drawing.Color.Red;
                return false;
            }
            // end already login

            string dayofweek = "";
            //Monday
            // var testDay = DateTime.Now.DayOfWeek.ToString();
            if (System.DateTime.Now.DayOfWeek.ToString() == "Monday")
            {
                dayofweek = "MonLogOut";
            }
            if (System.DateTime.Now.DayOfWeek.ToString() == "Tuesday")
            {
                dayofweek = "TueLogOut";
            }
            if (System.DateTime.Now.DayOfWeek.ToString() == "Wednesday")
            {
                dayofweek = "WedLogOut";
            }
            if (System.DateTime.Now.DayOfWeek.ToString() == "Thursday")
            {
                dayofweek = "ThuLogOut";
            }
            if (System.DateTime.Now.DayOfWeek.ToString() == "Friday")
            {
                dayofweek = "FriLogOut";
            }
            if (System.DateTime.Now.DayOfWeek.ToString() == "Saturday")
            {
                dayofweek = "SatLogOut";
            }


            // checking 15 min. Different +/-.
            // Begin
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString);
            conn.Open();
            SqlCommand cmd1 = new SqlCommand();
            SqlCommand cmd2 = new SqlCommand();

            cmd1.CommandText = "select " + dayofweek + ", Convert(Varchar, getdate(), 108), " +
                "DATEDIFF(MINUTE, Convert(Varchar, " + dayofweek + ", 108), Convert(Varchar, getdate(), 108) ) As DiffTime " +
                "from UserMaster where RowID = @RowID";
            string DefaultLateInOutMin = "10";

            cmd2.CommandText = "select * from SetupMaster where SetupDescription = '" + DefaultLateInOutMin;
            cmd2.Parameters.AddWithValue("@DefaultLateInOutMin", DefaultLateInOutMin);

            cmd1.Parameters.AddWithValue("@RowID", Session["RowID"]);
            cmd1.Connection = conn;

            SqlDataAdapter sda = new SqlDataAdapter(cmd1);
            //DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            sda.Fill(ds, "TblLogin");
            //ds.Tables["TblLogin"].Rows[0]["DiffTime"].ToString();
           

            string Timevalue;
            Timevalue = ds.Tables["TblLogin"].Rows[0]["DiffTime"].ToString();
            // value parsed, check if greater than 15

            int a = Convert.ToInt32(DefaultLateInOutMin);

            if (Convert.ToInt32(Timevalue) > a && txtRemarkOut.Text.ToString() == "")
            {
                Label5.Text = "You are late LogOut!! Please Enter reason";
                Label5.ForeColor = System.Drawing.Color.Red;
                txtRemarkOut.Focus();
                return false;
            }

            if (Convert.ToInt32(Timevalue) < a && txtRemarkOut.Text.ToString() == "")
            {
                Label5.Text = "You are early LogOut!! Please Enter reason";
                Label5.ForeColor = System.Drawing.Color.Red;
                txtRemarkOut.Focus();
                return false;
            } 
            return true;
        }       
    }
}