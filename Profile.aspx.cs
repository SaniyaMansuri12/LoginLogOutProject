using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace LoginLogoutPanelProject
{
    public partial class Admin : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString);
        string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        SqlCommand cmd = new SqlCommand();

        int RowID;
        protected void Page_Load(object sender, EventArgs e)
        {
            //filDrop();  
            if (Session["UserType"].ToString() == "Normal" && Request.QueryString["RowID"] != null)
            {
                RowID = Convert.ToInt32(Request.QueryString["RowID"]);
                //Response.Redirect("~/Dashboard.aspx");
            }
            else
            {
                if (Session["RowID"] != null && Request.QueryString["RowID"] != null)
                {
                    RowID = Convert.ToInt32(Request.QueryString["RowID"]);
                }
                else
                {
                    RowID = Convert.ToInt32(Session["RowID"]);
                }
            }

            if (!IsPostBack)
            {
                if (Session["RowID"] != null)
                {
                    string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                    string myquery = "Select * from UserMaster where RowID = " + RowID;
                    SqlConnection conm = new SqlConnection(cs);
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = myquery;
                    cmd.Connection = conm;
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    if (Session["UserType"] != null)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            txtUserId.Text = ds.Tables[0].Rows[0]["UserId"].ToString();
                            txtPassword.Text = ds.Tables[0].Rows[0]["Password"].ToString();
                            txtPassword.Attributes.Add("value", ds.Tables[0].Rows[0]["Password"].ToString());
                            txtMobile.Text = ds.Tables[0].Rows[0]["MobileNo"].ToString();
                            txtContact.Text = ds.Tables[0].Rows[0]["ContactNo"].ToString();
                            DropDownList1.SelectedItem.Text = ds.Tables[0].Rows[0]["Department"].ToString();
                            txtMachine.Text = ds.Tables[0].Rows[0]["MachineName"].ToString();
                            RadioButtonList1.SelectedValue = ds.Tables[0].Rows[0]["UserType"].ToString();
                            txtReporting.Text = ds.Tables[0].Rows[0]["ReportingTo"].ToString();
                            RadioButtonList2.SelectedValue = ds.Tables[0].Rows[0]["Status"].ToString();
                            txtPaidLeave.Text = ds.Tables[0].Rows[0]["PaidLeave"].ToString();
                        }
                        else
                        {
                            lblUserType.Visible = false;
                            RadioButtonList1.Visible = false;
                            RadioButtonList2.Visible = false;
                            txtReporting.Visible = false;
                            txtPaidLeave.Visible = false;
                            Label9.Visible = false;
                            Label10.Visible = false;
                            Label11.Visible = false;
                        }
                    }
                    if (Session["UserType"].ToString() == "Normal")
                    {
                        lblUserType.Visible = false;
                        RadioButtonList1.Visible = false;
                        RadioButtonList2.Visible = false;
                        txtReporting.Visible = false;
                        txtPaidLeave.Visible = false;
                        Label9.Visible = false;
                        Label10.Visible = false;
                        Label11.Visible = false;
                    }
                }
            }
            if (Session["RowID"] == null)
            {
                using (SqlConnection conn = new SqlConnection(cs))
                {
                    conn.Open();
                    string query1 = "Select * from UserMaster where RowID = @RowID";

                    SqlDataAdapter sda = new SqlDataAdapter(query1, conn);
                    DataSet ds1 = new DataSet();

                    sda.SelectCommand.Parameters.AddWithValue("@RowID", RowID);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);

                    lblUserType.Visible = false;
                    RadioButtonList1.Visible = false;
                    RadioButtonList2.Visible = false;
                    txtReporting.Visible = false;
                    txtPaidLeave.Visible = false;
                    Label9.Visible = false;
                    Label10.Visible = false;
                    Label11.Visible = false;
                }
            }
            //filDrop();
        }

        public void filDrop()
        {
            string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            {
                string com = "Select * from DepartmentSet";
                SqlDataAdapter adpt = new SqlDataAdapter(com, con);
                DataTable dt = new DataTable();
                adpt.Fill(dt);
                DropDownList1.DataSource = dt;
                DropDownList1.DataBind();
                DropDownList1.DataTextField = "Department";
                DropDownList1.DataValueField = "RowID";
                DropDownList1.DataBind();
               // DropDownList1.Items.Insert(0, new ListItem("Select"));
            }
        }
       
        string UserType;
        string Status;
        public void btnEdit_Click(object sender, EventArgs e)
        {     
            if (Session["RowID"] != null)
            {
                Update();
            }
            else
            {
                Insert();           
            }
        }
        
        public void Update()
        {          
            cmd.Connection = con;
            cmd.CommandText = "Update UserMaster set UserId ='" + txtUserId.Text + "'," +
                " Password = '" + txtPassword.Text + "', " +
                "MobileNo = '" + txtMobile.Text + "', " +
                "ContactNo = '" + txtContact.Text + "'," +
                " Department = '" + DropDownList1.SelectedItem.Text + "', " +
                "MachineName = '" + txtMachine.Text + "'," +
                "UserType = '" + RadioButtonList1.SelectedValue + "'," +
                " ReportingTo = '" + txtReporting.Text + "', " +
                "Status = '" + RadioButtonList2.SelectedValue + "', " +
                "PaidLeave = '" + txtPaidLeave.Text + "' where RowID = " + Request.QueryString["RowID"];

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();           
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Update Successfully!!')</script>");
        }
        
        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Dashboard.aspx");
        }

        public void Insert()
        {      
            UserType = "Normal";
            Status = "IsActive";

            cmd.Connection = con;
            cmd.CommandText = "Insert into UserMaster(UserId,Password,MobileNo,ContactNo,Department,Machinename,UserType,Reportingto,Status,PaidLeave) values " +
                "('" + txtUserId.Text + "','" + txtPassword.Text + "','"
                + txtMobile.Text + "','" + txtContact.Text + "','" + DropDownList1.SelectedItem.Text + "','"
                + txtMachine.Text + "','" + UserType + "','" + txtReporting.Text + "','"
                + Status + "','" + txtPaidLeave.Text + "')";

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();          
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Insert Successfully!!')</script>");
            Response.Redirect("~/Login/Login.aspx");
        }

        //    if (!IsPostBack)
        //    {
        //        if (Session["RowID"] != null)
        //        {
        //            int RowID = Convert.ToInt32(Session["RowID"].ToString());
        //            using (SqlConnection con = new SqlConnection(cs))
        //            {
        //                con.Open();
        //                string query = "Select * from UserMaster where RowID = @RowID";

        //                SqlDataAdapter sda = new SqlDataAdapter(query, con);
        //                DataSet ds = new DataSet();

        //                sda.SelectCommand.Parameters.AddWithValue("@RowID", RowID);
        //                DataTable dt = new DataTable();
        //                sda.Fill(dt);

        //                txtRowID.Text = dt.Rows[0][0].ToString();
        //                txtUserId.Text = dt.Rows[0][1].ToString();
        //                txtPassword.Text = dt.Rows[0][2].ToString();
        //                txtPassword.Attributes.Add("value", dt.Rows[0][2].ToString());
        //                txtMobile.Text = dt.Rows[0][3].ToString();
        //                txtContact.Text = dt.Rows[0][4].ToString();
        //                DropDownList1.Text = dt.Rows[0][5].ToString();
        //                txtMachine.Text = dt.Rows[0][6].ToString();
        //                Usertype = dt.Rows[0][7].ToString();
        //                txtReporting.Text = dt.Rows[0][8].ToString();
        //                Status = dt.Rows[0][9].ToString();
        //                txtPaidLeave.Text = dt.Rows[0][10].ToString();
        //            }
        //        }
    }
}
