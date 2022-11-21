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
    public partial class OtherBreakIn : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString);
        SqlCommand cmd = new SqlCommand();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        
        Boolean OtherBreakInValidation()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand();
            // validattion for already login
            //Begin
            cmd.CommandText = "SELECT * ,Convert(Varchar,OtherBreakIn,112), Convert(Varchar,getdate(),112) " +
                                "FROM TblLogin " +
                                "WHERE EmpId = @EmpId AND Convert(Varchar, OtherBreakIn,112) = Convert(Varchar, getdate(), 112)";       
            
            cmd.Parameters.AddWithValue("@EmpId", Session["RowID"]);

            cmd.Connection = con;
            SqlDataReader rd = cmd.ExecuteReader();
            if (rd.HasRows)
            {
                Label2.Visible = true;

                Label2.Text = "Already in OtherBreak Please Check!!!";
                Label2.ForeColor = System.Drawing.Color.Red;
                return false;
            }
            return true;
            // end already login
        }
        public void Update()
        {  
            cmd.Connection = con;
            cmd.CommandText = "Update TblLogin set OtherBreakIn =  GetDate()  " +
               "where Convert(Varchar, LogIn,112) = Convert(Varchar, getdate(), 112) and EmpId = " + Session["RowID"];

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            Response.Write("You are Break In Successfully");   
        } 

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (OtherBreakInValidation())
            {
                Update();
            }        
        }       
    }
}