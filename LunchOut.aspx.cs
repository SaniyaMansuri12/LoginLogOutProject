using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;


namespace LoginLogoutPanelProject
{
    public partial class LunchOut : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString);
        SqlCommand cmd = new SqlCommand();

        private string Login;

        protected void Page_Load(object sender, EventArgs e)
        {
            Login = Label1.Text = DateTime.Now.ToString("hh:mm:ss tt");
        }
        public void Update()
        {
            cmd.Connection = con;
            cmd.CommandText = "Update TblLogin set LunchOut =  GetDate()  " +
               "where Convert(Varchar, LogIn,112) = Convert(Varchar, getdate(), 112) and EmpId = " + Session["RowID"];
            //cmd.CommandText = "Update TblLogin set LunchOut =  GetDate() where EmpId = " + Session["RowID"];
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            Response.Write("You are lunchOut Successfully");
        }
        
        Boolean LunchOutValidation()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand();
            // validattion for already login
            //Begin
            cmd.CommandText = "SELECT  *, Convert(Varchar,LunchOut,112), Convert(Varchar, getdate(),112) " +
                                "FROM TblLogin " +
                                "WHERE EmpId = @EmpId AND Convert(Varchar, LunchOut,112) = Convert(Varchar, getdate(), 112)";

            cmd.Parameters.AddWithValue("@EmpId", Session["RowID"]);

            cmd.Connection = con;
            SqlDataReader rd = cmd.ExecuteReader();
            if (rd.HasRows)
            {
                Label2.Visible = true;
                Label2.Text = "Already Go for Lunch Please check Once!!!";
                Label2.ForeColor = System.Drawing.Color.Red;
                return false;
            }
           
            return true;
            // end already login
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (LunchOutValidation())
            {
                Update();
            }          
        }

        protected void Button1_Click1(object sender, EventArgs e)
        {

            Response.Redirect("~/Dashboard.aspx");
        }
    }
}