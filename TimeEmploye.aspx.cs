using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.UI.MobileControls;
using System.Drawing;
using System.Data;
using System.Configuration;

namespace LoginLogoutPanelProject
{
    public partial class TimeEmploye : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString);
        SqlCommand cmd = new SqlCommand();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] != null)
            {
                // Response.Write("Welcome " + " " + Session["UserId"].ToString());
                Label6.Text = Session["UserID"].ToString();
            }
            if (!IsPostBack)
            {          
                LoadData1();
                LoadData();
            }
        }

        public void LoadData()
        {
            string query = "Select * from UserMaster";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            DataSet ds = new DataSet();
            sda.Fill(ds, "UserMaster");
            GridView2.DataSource = ds.Tables["UserMaster"];   
            GridView2.DataBind();
        }

        public void LoadData1()
        {
            string query = "Select * from UserMaster";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            DataSet ds = new DataSet();
            sda.Fill(ds, "UserMaster");
            GridView1.DataSource = ds.Tables["UserMaster"];
            GridView1.DataBind();
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            cmd.Connection = con;
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                CheckBox chkb = (CheckBox)GridView1.Rows[i].Cells[0].FindControl("CheckBox7");
                if (chkb.Checked)
                {
                    string RowID = GridView1.Rows[i].Cells[1].Text;

                    cmd.CommandText = "Update UserMaster set MonLogIn = '" + txtLogIn.Text + "', " +
                        "MonLogOut = '" + txtLogOut.Text + "' ," +
                        " TueLogIn = '" + txtLogIn.Text + "', " +
                        "TueLogOut = '" + txtLogOut.Text + "', " +
                        " WedLogIn = '" + txtLogIn.Text + "', " +
                        "WedLogOut = '" + txtLogOut.Text + "', " +
                        "ThuLogIn = '" + txtLogIn.Text + "' ," +
                        "ThuLogOut = '" + txtLogOut.Text + "', " +
                        "FriLogIn = '" + txtLogIn.Text + "', " +
                        "FriLogOut = '" + txtLogOut.Text + "'," +
                        " SatLogIn = '" + txtLogIn.Text + "', " +
                        "SatLogOut = '" + txtLogOut.Text + "'  where RowID = " + RowID;

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    LoadData();
                    //Response.Write("Record Updated successfully");
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Record Updated Successfully!!')</script>");
                }
            }

            string days = "";

            for (int i = 0; i < CheckBoxList1.Items.Count; i++)
            {
                if (CheckBoxList1.Items[i].Selected)
                {
                    days = days + " " + CheckBoxList1.Items[i].Text;
                }
                Label5.Text = days;
            }
        }

        protected void btnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Dashboard.aspx");
        }   
    }
}

