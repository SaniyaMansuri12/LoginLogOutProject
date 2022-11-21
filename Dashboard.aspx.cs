using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using LoginLogoutPanelProject;
using System.Data;
using System.Configuration;

namespace LoginLogoutPanelProject
{
    public partial class Dashboard : System.Web.UI.Page
    {
        string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (UserVisible())
                {
                    //Session["UserType"] = "Normal";
                    if (Session["UserID"] != null)
                    {
                        // Response.Write("Welcome " + " " + Session["UserId"].ToString());
                        Label30.Text = Session["UserID"].ToString();
                    }
                }
            }

            if (!Page.IsPostBack)
            {
                string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
                using (SqlConnection con = new SqlConnection(cs))
                {
                    string com = "Select * from UserMaster order by UserID asc";
                    SqlDataAdapter adpt = new SqlDataAdapter(com, con);
                    DataTable dt = new DataTable();
                    adpt.Fill(dt);
                    DropDownList1.DataSource = dt;
                    DropDownList1.DataBind();
                    DropDownList1.DataTextField = "UserID";
                    DropDownList1.DataValueField = "RowID";
                    DropDownList1.DataBind();
                    DropDownList1.Items.Insert(0, new ListItem("Select"));
                }
            }
            FirstLoginLoad();
            LunchBreak();
            BreakTime();
            TotalOfDelay();
            FindDay();
            Absent();
            LoadGrid4();
        }

        public void FirstLoginLoad()
        {
            string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            {
                //SqlCommand cmd = new SqlCommand("Select month(Convert(Varchar, LogIn,112)), Year(Convert(Varchar, LogIn,112)) , Convert(Varchar, LogIn,104) " +
                //"As InDate, Convert(Varchar, LogIn, 108) As InTime, (Convert(Varchar, LogOut, 112)), Year(Convert(Varchar, LogOut, 112)), Convert(Varchar, LogOut, 104) " +
                //"As OutDate, Convert(Varchar, LogOut, 108) As OutTime, * from TblLogin where EmpId = " + Session["RowID"], con);

                // SqlCommand cmd = new SqlCommand("Select month(Convert(Varchar, LogIn,112)), Year(Convert(Varchar, LogIn,112)) , Convert(Varchar, LogIn,104) " +
                // "As InDate, Convert(Varchar, LogIn, 108) As InTime, (Convert(Varchar, LogOut, 112)), " +
                // "Year(Convert(Varchar, LogOut, 112)), Convert(Varchar, LogOut, 104) " +
                // "As OutDate, Convert(Varchar, LogOut, 108) As OutTime, Convert(Varchar, LunchIn, 108) As LunchIn, (Convert(Varchar, LogOut, 112)), " +
                // "Convert(Varchar, LunchOut, 108) As LunchOut, (Convert(Varchar, LogOut, 112)), " +
                // "Convert(Varchar, OtherBreakIn, 108) As OtherBreakIn, (Convert(Varchar, LogOut, 112)), " +
                // "Convert(Varchar, OtherBreakOut, 108) As OtherBreakOut, (Convert(Varchar, LogOut, 112))," +
                //" * from TblLogin where EmpId = " + Session["RowID"], con);

                SqlCommand cmd = new SqlCommand("SELECT LateIn, LateOut,LoginRemarks,LogOutRemarks,LunchInRemarks," +
                 "LunchBreakMin, AccessTime,OtherBreakRemarks,BreakTime," +
                 "month(Convert(Varchar, LogIn,112)), Year(Convert(Varchar, LogIn,112)) , Convert(Varchar, LogIn,104)" +
                "As InDate, Convert(Varchar, LogIn, 108) As InTime, (Convert(Varchar, LogOut, 112)), " +
                "Year(Convert(Varchar, LogOut, 112)), Convert(Varchar, LogOut, 104) " +
                "As OutDate, Convert(Varchar, MonLogIn, 108) As DefLogIn, " +
                "Convert(Varchar, MonLogOut, 108) As DefLogOut, " +
                "Convert(Varchar, LogOut, 108) As OutTime, Convert(Varchar, LunchIn, 108) As LunchIn, " +
                "(Convert(Varchar, LogOut, 112)),  Convert(Varchar, LunchOut, 108) As " +
                " LunchOut, (Convert(Varchar, LogOut, 112)), " +
                "Convert(Varchar, OtherBreakIn, 108) As OtherBreakIn, (Convert(Varchar, LogOut, 112)), " +
                "Convert(Varchar, OtherBreakOut, 108) As OtherBreakOut, (Convert(Varchar, LogOut, 112)), " +
                "TblLogin.EmpId, UserMaster.RowID  FROM TblLogin  " +
                "INNER JOIN UserMaster ON TblLogin.EmpId = UserMaster.RowID where EmpId = " + Session["RowID"], con);

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
        }

        public void LunchBreak()
        {
            string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            {
                //SqlCommand cmd1 = new SqlCommand("select LunchOut,LunchIn,LunchInRemarks,LunchBreakMin,AccessTime," +
                //    "Convert(Varchar, getdate(), 108), " +
                //"DATEDIFF(MINUTE, Convert(Varchar, LunchOut, 108), Convert(Varchar, LunchIn, 108)) As DiffTime " +
                //"from TblLogin where EmpId = " + Session["RowID"], con);

                SqlCommand cmd1 = new SqlCommand("select Convert(Varchar, LogIn,104) " +
                 "As InDate,LunchOut as lunchout1, LunchIn as lunchin1, LunchInRemarks, LunchBreakMin, AccessTime, " +
                "Convert(Varchar, LunchOut, 108) As LunchOut, (Convert(Varchar, LogOut, 112)), " +
                 "Convert(Varchar, LunchIn, 108) As LunchIn, (Convert(Varchar, LogOut, 112)), " +
                "Convert(Varchar, getdate(), 108), " +
                "DATEDIFF(MINUTE, Convert(Varchar, LunchOut, 108), Convert(Varchar, LunchIn, 108)) As DiffTime, " +
                "Convert(Varchar, OtherBreakIn, 108) As OtherBreakIn, (Convert(Varchar, LogOut, 112)), " +
                "Convert(Varchar, OtherBreakOut, 108) As OtherBreakOut, (Convert(Varchar, LogOut, 112))," +
                 "* from TblLogin where EmpId = " + Session["RowID"], con);

                SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
                DataTable dt1 = new DataTable();
                sda1.Fill(dt1);
                GridView2.DataSource = dt1;
                GridView2.DataBind();

                GridView3.DataSource = dt1;
                GridView3.DataBind();              

                if (dt1.Rows.Count > 0)
                {
                    //GridView2.FooterRow.Cells[5].Text = "Access Time";
                    //GridView2.FooterRow.Cells[6].Text = dt1.Compute("Sum(AccessTime)", "").ToString();

                    GridView2.FooterRow.Cells[3].Text = "Total Time";
                    GridView2.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                    GridView2.FooterRow.Cells[4].Text = dt1.Compute("Sum(DiffTime)", "").ToString();

                    //GridView3.FooterRow.Cells[3].Text = "Total Time";
                    //GridView3.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                    //GridView3.FooterRow.Cells[4].Text = dt1.Compute("Sum(DiffTime)", "").ToString();                  

                    int LunchBreakMin = 0;

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = "select * from SetupMaster where SetupDescription = 'LunchBreakMin'";

                    con.Open();
                    cmd.ExecuteNonQuery();
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    LunchBreakMin = Convert.ToInt32(dt.Rows[0]["Setupfld"].ToString());
                    con.Close();

                    int AccessTimeDiff = 0;                 
                    if (dt1.Rows[0]["lunchin1"].ToString() != "")
                    {
                        for (int i = 0; i < GridView2.Rows.Count; i++)
                        {
                            if (GridView2.Rows.Count > 0)
                            {
                                GridView2.Rows[i].Cells[5].Text = LunchBreakMin.ToString();
                                GridView2.Rows[i].Cells[4].Text = dt1.Rows[i]["DiffTime"].ToString();
                                {
                                    GridView2.FooterRow.Cells[5].Text = "Access Time";
                                    {
                                        if (dt1.Rows[i]["lunchin1"].ToString() != "")
                                        {
                                            GridView2.Rows[i].Cells[6].Text = (Convert.ToInt32(GridView2.Rows[i].Cells[4].Text.ToString())
                                                                               - LunchBreakMin).ToString();

                                            AccessTimeDiff = AccessTimeDiff + Convert.ToInt32(GridView2.Rows[i].Cells[6].Text.ToString());
                                        }
                                    }
                                }
                            }
                        }
                    }
                    GridView2.FooterRow.Cells[6].Text = AccessTimeDiff.ToString();
                }                
            }
        }

        public void BreakTime()
        {  
            string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            {
                //SqlCommand cmd1 = new SqlCommand("select OtherBreakOut, OtherBreakIn,OtherBreakRemarks, BreakTime, AccessTime, " +
                //    "Convert(Varchar, getdate(), 108), " +
                //"DATEDIFF(MINUTE, Convert(Varchar, OtherBreakOut, 108), Convert(Varchar, OtherBreakIn, 108)) As DiffTime " +
                //"from TblLogin where EmpId = " + Session["RowID"], con);

                SqlCommand cmd1 = new SqlCommand("select Convert(Varchar, LogIn,104) " +
                "As InDate,OtherBreakRemarks,  BreakTime, AccessTime, " +
                "Convert(Varchar, OtherBreakOut, 108) As OtherBreakOut, (Convert(Varchar, LogOut, 112)), " +
                "Convert(Varchar, OtherBreakIn, 108) As OtherBreakIn, (Convert(Varchar, LogOut, 112)), " +
                "Convert(Varchar, getdate(), 108), " +
               "DATEDIFF(MINUTE, Convert(Varchar, OtherBreakOut, 108), Convert(Varchar, OtherBreakIn, 108)) As DiffTime " +
                "from TblLogin where EmpId = " + Session["RowID"], con);

                SqlDataAdapter sda = new SqlDataAdapter(cmd1);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                GridView3.DataSource = dt;
                GridView3.DataBind();
               
                if (dt.Rows.Count > 0)
                {
                    GridView3.FooterRow.Cells[3].Text = "Total Time";
                    GridView3.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                    GridView3.FooterRow.Cells[4].Text = dt.Compute("Sum(DiffTime)", "").ToString();      
                }
            }       
        } 

        public void LoadGrid4()
        {   
            string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlDataAdapter sda = new SqlDataAdapter("Select * from HolidayMaster", con);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                GridView4.DataSource = ds;
                GridView4.DataBind();
            }     
        }

        int RowID;
        protected void btnEdit_Click(object sender, EventArgs e)
        {       
            Response.Redirect("~/Profile.aspx");
        }

        string UserType = "Normal";
        Boolean UserVisible()
        {
            string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            {
                if (Session["UserType"].ToString() == "Normal")
                {
                    string UserType = Session["UserType"].ToString();
                    using (SqlConnection con = new SqlConnection(cs))
                    {
                        con.Open();
                        string query = "Select * from UserMaster where UserType = @UserType";

                        SqlDataAdapter sda = new SqlDataAdapter(query, con);
                        DataSet ds = new DataSet();

                        sda.SelectCommand.Parameters.AddWithValue("@UserType", UserType);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);

                        UserType = dt.Rows[0][7].ToString();
                        if (UserType == "Normal")
                        {
                            HyperLink10.Visible = false;
                            HyperLink7.Visible = false;
                            HyperLink9.Visible = false;
                            DropDownList1.Visible = false;
                            btnGO.Visible = false;
                            return true;
                        }
                    }
                }
            }
            return true;
        }

        protected void btnGO_Click(object sender, EventArgs e)
        {   
            string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("Select month(Convert(Varchar, LogIn, 112))," +
                    "Year(Convert(Varchar, LogIn, 112)), Convert(Varchar, LogIn, 104) " +
                "As InDate, Convert(Varchar, LogIn, 108) As InTime, (Convert(Varchar, LogOut, 112)), " +
                "Year(Convert(Varchar, LogOut, 112)), Convert(Varchar, LogOut, 104) " +
                "As OutDate, DATEDIFF(MINUTE, Convert(Varchar, LunchOut, 108), Convert(Varchar, LunchIn, 108))" +
                " As DiffTime,Convert(Varchar, LogOut, 108)" +
                "As OutTime, Convert(Varchar, LunchIn, 108) As LunchIn, (Convert(Varchar, LogOut, 112)), " +
                "Convert(Varchar, LunchOut, 108) As LunchOut, (Convert(Varchar, LogOut, 112)), " +
                "Convert(Varchar, OtherBreakIn, 108) As OtherBreakIn, (Convert(Varchar, LogOut, 112)), " +
                "Convert(Varchar, OtherBreakOut, 108) As OtherBreakOut, (Convert(Varchar, LogOut, 112))," +
                " * from TblLogin where EmpId = " + DropDownList1.SelectedValue + "" +
                " Select Year = " + txtSelectYear.Text + "", con);

                // SqlCommand cmd = new SqlCommand("SELECT LateIn, LateOut,LoginRemarks,DiffTime,LogOutRemarks,LunchInRemarks," +
                // "LunchBreakMin, AccessTime,OtherBreakRemarks,BreakTime," +
                // "month(Convert(Varchar, LogIn,112)), Year(Convert(Varchar, LogIn,112)) , Convert(Varchar, LogIn,104)" +
                // "As InDate, Convert(Varchar, LogIn, 108) As InTime, (Convert(Varchar, LogOut, 112)), " +
                //   "Year(Convert(Varchar, LogOut, 112))," +
                // "Convert(Varchar, getdate(), 108), DATEDIFF(MINUTE, Convert(Varchar, LunchOut, 108)," +
                // "Convert(Varchar, LunchIn, 108)) As DiffTime, " +
                //" Convert(Varchar, LogOut, 104) As OutDate, Convert(Varchar, MonLogIn, 108) As DefLogIn, " +
                //"Convert(Varchar, MonLogOut, 108) As DefLogOut, Convert(Varchar, LogOut, 108) As OutTime, " +
                //"Convert(Varchar, LunchIn, 108) As LunchIn, " +
                //"(Convert(Varchar, LogOut, 112)),  Convert(Varchar, LunchOut, 108) As " +
                //" LunchOut, (Convert(Varchar, LogOut, 112)), " +
                //"Convert(Varchar, OtherBreakIn, 108) As OtherBreakIn, (Convert(Varchar, LogOut, 112)), " +
                //"Convert(Varchar, OtherBreakOut, 108) As OtherBreakOut, (Convert(Varchar, LogOut, 112)), " +
                //"TblLogin.EmpId, UserMaster.RowID  FROM TblLogin  " +
                //"INNER JOIN UserMaster ON TblLogin.EmpId = UserMaster.RowID where EmpId = " + DropDownList1.SelectedValue + "", con);

                SqlDataAdapter Adpt = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                Adpt.Fill(dt);
                GridView1.DataSource = dt;
                GridView1.DataBind();
                GridView2.DataSource = dt;
                GridView2.DataBind();
                GridView3.DataSource = dt;
                GridView3.DataBind();
            }
        }

        int AccessTimeDiff = 0;
        public void TotalOfDelay()
        {   
            string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("select LunchIn as lunchin1,* from TblLogin where EmpId = " 
                                                                 + Session["RowID"], con);

                SqlDataAdapter sda1 = new SqlDataAdapter(cmd);
                DataTable dt2 = new DataTable();
                sda1.Fill(dt2);

                for (int i = 0; i < GridView2.Rows.Count; i++)
                {
                    //if (dt2.Rows[i]["DiffTime"].ToString() != "")
                    {
                        if (dt2.Rows[i]["lunchin1"].ToString() != "")
                        {
                            AccessTimeDiff = AccessTimeDiff + Convert.ToInt32(GridView2.Rows[i].Cells[6].Text.ToString());
                            GridView2.FooterRow.Cells[6].Text = AccessTimeDiff.ToString();
                           // if (dt2.Rows[i]["DiffTime"].ToString() == "")
                            {
                                int num1, num2, total;
                                num1 = Convert.ToInt32(GridView3.FooterRow.Cells[4].Text.ToString());
                                num2 = Convert.ToInt32(AccessTimeDiff.ToString());
                                total = num1 + num2;
                                lblDelay.Text = total.ToString();
                            }
                        }
                    }
                }               
            }
        }

        public void FindDay()
        { 
            string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("Select  DAY(EOMONTH('2022-11-01')) As Days ,dbo.f_count_sundays(2022,11) AS Sundays", con);
                //SqlCommand cmd = new SqlCommand("Select  DAY(EOMONTH('" + txtSelectYear.Text + "-01')) As Days ,dbo.f_count_sundays(" + + ","+ +") AS Sundays ", con);
                //cmd.CommandType = System.Data.CommandType.StoredProcedure;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                dr.Read();
                lblDays.Text = dr["Days"] + "";
                lblSundays.Text = dr["Sundays"] + "";
            }
        }

        protected void GridView1_DataBound(object sender, EventArgs e)
        {
            lblPresentDay.Text = " " + (GridView1.DataSource as DataTable).Rows.Count;
        }

        public void Absent()
        { 
            string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            {
                int Days, PresentDay, Holidays, Sundays, total;
                SqlCommand cmd1 = new SqlCommand("select * from TblLogin", con);
                con.Open();
                SqlDataReader dr = cmd1.ExecuteReader();
                dr.Read();
                Days = Convert.ToInt32(lblDays.Text);
                PresentDay = Convert.ToInt32(lblPresentDay.Text);
                Holidays = Convert.ToInt32(lblHolidays.Text);
                Sundays = Convert.ToInt32(lblSundays.Text);
               
                total = Days - PresentDay - Holidays - Sundays;
                lblAbsentDay.Text = total.ToString();
            }
        }     
    }     
}


    
