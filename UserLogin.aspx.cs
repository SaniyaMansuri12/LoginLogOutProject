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
    public partial class DefaultTime : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString);
        SqlCommand cmd = new SqlCommand();

        private string Login;
        protected void Page_Load(object sender, EventArgs e)
        {
            Login = Label1.Text = DateTime.Now.ToString("hh:mm:ss tt");         
        }
       
        public void Insert()
        {
            cmd.Connection = con;
            cmd.CommandText = "Insert into TblLogin(EmpId,LoginRemarks,LogIn)values('" + Session["RowID"] + "','" + txtRemarks.Text + "', GetDate()) ";
            // cmd.CommandText = "Insert into TblLogin values ('" + EmpId() + "','" + GetDate()
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            Response.Write("Welcome!!! You are Login successfully");      
        }

        int DefaultLateInOutMin = 10;
        Boolean LogInValidation()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand();
            // validattion for already login
            //Begin
            cmd.CommandText = "SELECT * ,Convert(Varchar,LogIn,112), Convert(Varchar,getdate(),112) " +
                                "FROM TblLogin " +
                                "WHERE EmpId = @EmpId AND Convert(Varchar, LogIn,112) = Convert(Varchar, getdate(), 112)";

            cmd.Parameters.AddWithValue("@EmpId", Session["RowID"]);

            cmd.Connection = con;
            SqlDataReader rd = cmd.ExecuteReader();
            if (rd.HasRows)
            {
                Label2.Visible = true;
                Label2.Text = "You are already LogIn Please check!!!";
                Label2.ForeColor = System.Drawing.Color.Red;
                return false;
            }

            // end already login
            // checking 15 min. Different +/-.
            // Begin
            string dayofweek = "";
            //Monday
            // var testDay = DateTime.Now.DayOfWeek.ToString();
            if (System.DateTime.Now.DayOfWeek.ToString() == "Monday")
            {
                dayofweek = "MonLogIn";
            }
            if (System.DateTime.Now.DayOfWeek.ToString() == "Tuesday")
            {
                dayofweek = "TueLogIn";
            }
            if (System.DateTime.Now.DayOfWeek.ToString() == "Wednesday")
            {
                dayofweek = "WedLogIn";
            }
            if (System.DateTime.Now.DayOfWeek.ToString() == "Thursday")
            {
                dayofweek = "ThuLogIn";
            }
            if (System.DateTime.Now.DayOfWeek.ToString() == "Friday")
            {
                dayofweek = "FriLogIn";
            }
            if (System.DateTime.Now.DayOfWeek.ToString() == "Saturday")
            {
                dayofweek = "SatLogIn";
            }
            
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString);
            conn.Open();
            
            SqlCommand cmd1 = new SqlCommand();
            SqlCommand cmd2 = new SqlCommand();

            cmd1.CommandText = "select " + dayofweek + ", Convert(Varchar, getdate(), 108), " +
              "DATEDIFF(MINUTE, Convert(Varchar, " + dayofweek + ", 108), Convert(Varchar, getdate(), 108) ) As DiffTime " +
              "from UserMaster where RowID = @RowID";

            cmd2.CommandText = "select * from SetupMaster where SetupDescription = '" + DefaultLateInOutMin;
            cmd2.Parameters.AddWithValue("@DefaultLateInOutMin", DefaultLateInOutMin);

            cmd1.Parameters.AddWithValue("@RowID", Session["RowID"]);
            cmd1.Connection = conn;

            SqlDataAdapter sda = new SqlDataAdapter(cmd1);
            SqlDataAdapter sda1 = new SqlDataAdapter(cmd2);
            DataSet ds = new DataSet();
            sda.Fill(ds, "TblLogin");

         
            string Timevalue;
            Timevalue = ds.Tables["TblLogin"].Rows[0]["DiffTime"].ToString();
            // value parsed, check if greater than 15

            int SetUp = Convert.ToInt32(DefaultLateInOutMin);

            if (Convert.ToInt32(Timevalue) > SetUp && txtRemarks.Text.ToString() == "")
            {
                Label3.Text = "You are late!! Please Enter reason";
                Label3.ForeColor = System.Drawing.Color.Red;    
                txtRemarks.Focus();
                return false;
            }

            if (Convert.ToInt32(Timevalue) < -SetUp && txtRemarks.Text.ToString() == "")
            {
                Label3.Text = "You are early!! Please Enter reason";
                Label3.ForeColor = System.Drawing.Color.Red;
                txtRemarks.Focus();
                return false;
            }
            return true;        
        }    
        
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (LogInValidation())
            {
               Insert();
            }       
        }         
    }
}