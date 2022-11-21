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
    public partial class ApplicationLeave : System.Web.UI.Page
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
                Calendar2.Visible = false;    
            }

            if (Page.IsPostBack == false)
            {
                LoadGrid();
                Calendar1.Visible = false;
                Calendar2.Visible = false;
            }   
        }

        public void LoadGrid()
        {
            SqlDataAdapter sda = new SqlDataAdapter("Select * from LeaveAppMaster", con);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            GridView1.DataSource = ds;
            GridView1.DataBind();
        }

        Boolean LeaveValidation()
        {
            SqlCommand cmd = new SqlCommand("Select * from LeaveAppMaster where  FromDate = '" + txtFromDate.Text 
                                                                                               + "'", con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {              
                Label1.Text = "This Date is already Here";
                //Label1.Text = "Please fill all Field";
                Label1.ForeColor = System.Drawing.Color.Red;
                return false;
            }
            return true;
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            if (LeaveValidation())
            {
                cmd.Connection = con;
                cmd.CommandText = "Insert into LeaveAppMaster(ApplyDate,FromDate,ToDate,LeaveType,ReasonForLeave) values " +
                                                                   "('" + txtApplyDate.Text + "','"
                                                                        + txtFromDate.Text + "','"

                                                                        + txtToDate.Text + "','"
                                                                        + DropDownList1.SelectedItem.Text + "','"
                                                                        + txtLeaveRemark.Text + "')";

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                // Response.Write("Your leave successfully done");
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Holidays inserted Successfully!!')</script>");
               
                LoadGrid();
                Clear();
            }     
        }

        protected void btnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Dashboard.aspx");
        }

        public void Clear()
        {
            txtFromDate.Text = "";
            txtToDate.Text = "";
            txtLeaveRemark.Text = "";        
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
            txtFromDate.Text = Calendar1.SelectedDate.ToString("dd.MM.yyyy");
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

        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {
            if (Calendar2.Visible)
            {
                Calendar2.Visible = false;
            }
            else
            {
                Calendar2.Visible = true;
            }
        }

        protected void Calendar2_DayRender(object sender, DayRenderEventArgs e)
        {
            if (e.Day.IsOtherMonth || e.Day.IsWeekend)
            {
                e.Day.IsSelectable = false;
                e.Cell.BackColor = System.Drawing.Color.LightBlue;
            }
        }

        protected void Calendar2_SelectionChanged(object sender, EventArgs e)
        {
            txtToDate.Text = Calendar2.SelectedDate.ToString("dd.MM.yyyy");
            Calendar2.Visible = false;
        }
    }
}