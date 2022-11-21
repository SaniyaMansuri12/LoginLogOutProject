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
    public partial class HolidayMaster1 : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString);
        SqlCommand cmd = new SqlCommand();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] != null)
            {
                // Response.Write("Welcome " + " " + Session["UserId"].ToString());
                Label2.Text = Session["UserID"].ToString();
            }

            if (!IsPostBack)
            {
                Calendar1.Visible = false;
                
            }
            if (Page.IsPostBack == false)
            {
                LoadGrid();
            }
        }

        public void LoadGrid()
        {
            SqlDataAdapter sda = new SqlDataAdapter("Select * from HolidayMaster", con);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            GridView1.DataSource = ds;
            GridView1.DataBind();
        }

        Boolean HolidayValidation()
        {
            SqlCommand cmd = new SqlCommand("Select * from  HolidayMaster where  DateOfHoliday = '"
                                                                                   + txtDate.Text + "'", con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            //Label1.Text = "Please Fill all field";
            if (dt.Rows.Count > 0)
            {
                Label1.Text = "This Date is already Here";
                
                Label1.ForeColor = System.Drawing.Color.Red;
                return false;
            } 
            return true;
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            if (HolidayValidation())
            {
                cmd.Connection = con;
                cmd.CommandText = "Insert into HolidayMaster(DateOfHoliday,Reasonforholiday) values " +
                                                                        "('" + txtDate.Text + "','"
                                                                        +txtReason.Text + "')";
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                // Response.Write("Holidays inserted successfully");
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Holidays inserted Successfully!!')</script>");
                LoadGrid();
                Clear();
            }
        }

        public void Clear()
        {
            txtDate.Text = "";
            txtReason.Text = "";
        }

        protected void btnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Dashboard.aspx");
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string DateOfHoliday = GridView1.Rows[e.RowIndex].Cells[1].Text;
            string s = "delete from HolidayMaster where DateOfHoliday = '" + DateOfHoliday + "'";
            SqlCommand cmd = new SqlCommand(s, con);
            cmd.CommandType = CommandType.Text;
            con.Open();
            cmd.ExecuteNonQuery();
            LoadGrid();
            Clear();
            con.Close();
        }

        protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            GridView1.SelectedIndex = e.NewSelectedIndex;
            LoadGrid();
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            if (Calendar1.Visible)
            {
                Calendar1.Visible = false;
            }
            else
            {
                Calendar1.Visible = true;          
            }
        }

        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            txtDate.Text = Calendar1.SelectedDate.ToString("dd.MM.yyyy");
            Calendar1.Visible = false;     
        }

        protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
        {
            if (e.Day.IsOtherMonth || e.Day.IsWeekend)
            {
                e.Day.IsSelectable = false;
                e.Cell.BackColor = System.Drawing.Color.LightBlue;
            }
        }
    }
}