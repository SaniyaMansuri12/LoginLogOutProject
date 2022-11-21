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
    public partial class LunchIn : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString);
        SqlCommand cmd = new SqlCommand();

        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public void Update()
        {
            cmd.Connection = con;
            cmd.CommandText = "Update TblLogin set LunchIn =  GetDate() , " +
                              "LunchInRemarks = '" + txtLunchIn.Text + "'" +
                              " where Convert(Varchar, LogIn,112) = Convert(Varchar, getdate(), 112)" +
                              " and EmpId = " + Session["RowID"];
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            Response.Write("You are LunchIn Successfully");
        }

        Boolean LunchInValidation()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand();
            // validattion for already login
            //Begin
            cmd.CommandText = "SELECT * ,Convert(Varchar,LunchIn,112), Convert(Varchar,getdate(),112) " +
                                "FROM TblLogin " +
                           "WHERE EmpId = @EmpId AND Convert(Varchar, LunchIn,112) = Convert(Varchar, getdate(), 112)";

            cmd.Parameters.AddWithValue("@EmpId", Session["RowID"]);

            cmd.Connection = con;
            SqlDataReader rd = cmd.ExecuteReader();
            if (rd.HasRows)
            {
                Label2.Visible = true;

                //Label2.Text = "Please Check for login";
                Label2.Text = "Already in LunchOut Please Check!!!";
                Label2.ForeColor = System.Drawing.Color.Red;
                return false;
            }
                
            
            // end already login
            // checking 45 min. Different +/-.
            // Begin
            int LunchBreakMin = 30;

            SqlConnection connn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString);
            connn.Open();
            SqlCommand cmdd1 = new SqlCommand();
            SqlCommand cmd2 = new SqlCommand();

            cmdd1.CommandText = "select LunchOut, Convert(Varchar, getdate(), 108), " +
                "DATEDIFF(MINUTE, Convert(Varchar, LunchOut, 108), Convert(Varchar, getdate(), 108) ) As DiffTime " +
                "from TblLogin where EmpId = @EmpId";

            cmdd1.Parameters.AddWithValue("@EmpId", Session["RowID"]);
            cmdd1.Connection = connn;

            cmd2.CommandText = "select * from SetupMaster where SetupDescription = '" + LunchBreakMin + "'";
            cmd2.Parameters.AddWithValue("@LunchBreakMin", LunchBreakMin);

            SqlDataAdapter sda = new SqlDataAdapter(cmdd1);

            DataSet ds = new DataSet();
            sda.Fill(ds, "TblLogin");

            string Timevalue;
            
            Timevalue = ds.Tables["TblLogin"].Rows[0]["DiffTime"].ToString();
           
            int ValueOfSatupFld = Convert.ToInt32(LunchBreakMin);

            if (Convert.ToInt32(Timevalue) > ValueOfSatupFld && txtLunchIn.Text.ToString() == "")
            {
                Label3.Text = "You are late!! Please Enter reason";
                Label3.ForeColor = System.Drawing.Color.Red;
                txtLunchIn.Focus();
                return false;
            }
            // value parsed, check if greater than 45
            //int ValueOfSatupFld = Convert.ToInt32(LunchBreakMin);

            //if (Convert.ToInt32(Timevalue) > ValueOfSatupFld && txtLunchIn.Text.ToString() == "")
            //{
            //    Label3.Text = "You are late!! Please Enter reason";
            //    Label3.ForeColor = System.Drawing.Color.Red;
            //    txtLunchIn.Focus();
            //    return false;
            //}
            return true;
        }

        //Boolean CheckLogin()
        //{
        //    string dayofweek = "";
        //    //Monday
        //    // var testDay = DateTime.Now.DayOfWeek.ToString();
        //    if (System.DateTime.Now.DayOfWeek.ToString() == "Monday")
        //    {
        //        dayofweek = "MonLogIn";
        //    }
        //    if (System.DateTime.Now.DayOfWeek.ToString() == "Tuesday")
        //    {
        //        dayofweek = "TueLogIn";
        //    }
        //    if (System.DateTime.Now.DayOfWeek.ToString() == "Wednesday")
        //    {
        //        dayofweek = "WedLogIn";
        //    }
        //    if (System.DateTime.Now.DayOfWeek.ToString() == "Thursday")
        //    {
        //        dayofweek = "ThuLogIn";
        //    }
        //    if (System.DateTime.Now.DayOfWeek.ToString() == "Friday")
        //    {
        //        dayofweek = "FriLogIn";
        //    }
        //    if (System.DateTime.Now.DayOfWeek.ToString() == "Saturday")
        //    {
        //        dayofweek = "SatLogIn";
        //    }

        //    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString);
        //    conn.Open();

        //    SqlCommand cmd1 = new SqlCommand();

        //    cmd1.CommandText = "select " + dayofweek + ", Convert(Varchar, getdate(), 108), " +
        //      "DATEDIFF(MINUTE, Convert(Varchar, " + dayofweek + ", 108), Convert(Varchar, getdate(), 108) ) As DiffTime " +
        //      "from UserMaster where RowID = @RowID";

        //    cmd1.Parameters.AddWithValue("@RowID", Session["RowID"]);
        //    cmd1.Connection = conn;

        //    SqlDataReader rd = cmd1.ExecuteReader();
        //    Label2.Visible = true;
        //    Label2.Text = "Please check for login";
        //    Label2.ForeColor = System.Drawing.Color.Red;
        //    return true;
        

        protected void Button1_Click(object sender, EventArgs e)
        {
            // if (CheckLogin())
            if (LunchInValidation())
            {

                Update();
            }
        }
    }
}

    
