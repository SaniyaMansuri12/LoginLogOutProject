using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace LoginLogoutPanelProject
{
    public partial class UserMaster : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] != null)
            {
               // Response.Write("Welcome " + " " + Session["UserId"].ToString());
                Label1.Text = Session["UserID"].ToString();
            }

            else
            {
                //Response.Redirect("Admin/AdminLogin.aspx");
            }
            UserGrid();
        }

        public void UserGrid()
        {
            string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("select * from UserMaster order by UserId", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                GridView2.DataSource = dt;
                GridView2.DataBind();          
            }
        }

       // int RowID;
        string RowID;
        protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
        {         
            RowID = GridView2.SelectedRow.Cells[1].Text;
            Response.Redirect("~/Profile.aspx?RowID=" + RowID);
        }
    }
}
