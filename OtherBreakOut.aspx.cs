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
    public partial class OtherBreakOut : System.Web.UI.Page
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
            //cmd.CommandText = "Update TblLogin set OtherBreakOut =  GetDate()  " +

            //   "where Convert(Varchar, LogIn,112) = Convert(Varchar, getdate(), 112) and EmpId = " + Session["RowID"];

            cmd.CommandText = "Update TblLogin set OtherBreakOut =  GetDate() , " +
             "OtherBreakRemarks = '" + txtBreakOutRemark.Text + "' " +
             "where Convert(Varchar, LogIn,112) = Convert(Varchar, getdate(), 112) and EmpId = " + Session["RowID"];
            //cmd.CommandText = "Update TblLogin set OtherBreakOut =  GetDate() where EmpId = " + Session["RowID"];
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            Response.Write("You are breakOut Successfully");       
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (BreakOutValidation())
            {
                Update();
            }   
        }

      
        Boolean BreakOutValidation()
        {       
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand();
            // validattion for already login
            //Begin
            cmd.CommandText = "SELECT * ,Convert(Varchar,OtherBreakOut,112), Convert(Varchar,getdate(),112) " +
                                "FROM TblLogin " +
                                "WHERE EmpId = @EmpId AND Convert(Varchar, OtherBreakOut,112) = Convert(Varchar, getdate(), 112)";


            cmd.Parameters.AddWithValue("@EmpId", Session["RowID"]);
           
            cmd.Connection = con;
            SqlDataReader rd = cmd.ExecuteReader();
            if (rd.HasRows)
            {
                Label2.Visible = true;
                Label2.Text = "Already in OtherBreakOut Please Check!!!";
                Label2.ForeColor = System.Drawing.Color.Red;
                return false;
            }
          

            // end already login

            // checking 45 min. Different +/-.
            // Begin
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString);
            conn.Open();
            SqlCommand cmd1 = new SqlCommand();
            cmd1.CommandText = "select OtherBreakOut, Convert(Varchar, getdate(), 108), " +
                "DATEDIFF(MINUTE, Convert(Varchar, OtherBreakOut, 108), Convert(Varchar, getdate(), 108) ) As DiffTime " +
                "from TblLogin where EmpId = @EmpId";


            cmd1.Parameters.AddWithValue("@EmpId", Session["RowID"]);
            cmd1.Connection = conn;

            SqlDataAdapter sda = new SqlDataAdapter(cmd1);
           
            DataSet ds = new DataSet();
            sda.Fill(ds, "TblLogin");

            
            // value parsed, check if greater than 15
            if (txtBreakOutRemark.Text.ToString() == "")
            {
                Label3.Text = "You want break!!!Please Enter reason";
                Label3.ForeColor = System.Drawing.Color.Red;
                txtBreakOutRemark.Focus();
                return false;
            }
            return true;
        }   
    }
}