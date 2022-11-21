<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApplicationLeave.aspx.cs" Inherits="LoginLogoutPanelProject.ApplicationLeave" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

  
    
     <style type="text/css">
        .auto-style1 {
            margin-left: 0px;
            
        }
        .table{
            text-align : center;
        }
        .auto-style3 {
            width: 340px;
            
            text-align:center;
        }
        h1{
            text-align:center;
            font-family:Arial, Helvetica, sans-serif;
            font-weight:bold;
        }
         .auto-style4 {
             margin-left: 656px;
             margin-top: 0px;
         }
         .auto-style5 {
             margin-top: 6px;
         }
         .auto-style6 {
             margin-left: 561px;
         }
         .auto-style7 {
             margin-left: 0px;
             margin-top: 2px;
             margin-right: 0px;
         }
         .auto-style8 {
             margin-top: 0px;
         }
         .auto-style11 {
             width: 340px;
             text-align: center;
             height: 37px;
         }
         .auto-style12 {
             height: 37px;
         }
         .auto-style13 {
             width: 340px;
             text-align: center;
             height: 65px;
         }
         .auto-style14 {
             height: 65px;
         }
         .auto-style15 {
             width: 340px;
             text-align: center;
             height: 54px;
         }
         .auto-style16 {
             height: 54px;
         }
         .auto-style17 {
             margin-top: 7px;
             margin-left: 2px;
         }
         .auto-style18 {
             margin-top: 14px;
         }
         .auto-style19 {
             width: 340px;
             text-align: center;
             height: 24px;
         }
         .auto-style20 {
             height: 24px;
         }
        .auto-style2 {
            height: 47px;
            margin-top: 0px;
            margin-bottom: 0px;
        }
        </style>



</head>
<body>
    <form id="form1" runat="server">
        <div>


        <h2 class="auto-style2" style="background-color: #FFFF00; text-align: center; color: #0000FF; font-family: 'Franklin Gothic Medium', 'Arial Narrow', Arial, sans-serif;">Welcome
            <asp:Label ID="Label2" runat="server"></asp:Label>
        </h2>
        
            <br />
             <table align="center">
               <h1>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Leave Application</h1>

                <tr>
                    <td class="auto-style19">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Apply Date</td>
                     <td class="auto-style20">
                         <asp:TextBox ID="txtApplyDate" runat="server" placeholder="Enter ApplyDate" Height="26px" Width="108px" CssClass="auto-style1" ReadOnly="True">19.10.2022</asp:TextBox>
                         <br />
                         <br /></td>
                </tr>
                 <tr>
                    <td class="auto-style11">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; From Date</td>
                     <td class="auto-style12"><asp:TextBox ID="txtFromDate" runat="server"  placeholder="Enter FromDate"  Height="28px" Width="216px" CssClass="auto-style7" ></asp:TextBox>
                         <asp:ImageButton ID="ImageButton1" runat="server" CssClass="auto-style18" Height="30px" ImageUrl="~/Images/download.png" OnClick="ImageButton1_Click" Width="29px" />
                         <asp:Calendar ID="Calendar1" runat="server" CssClass="auto-style1" Width="224px" OnDayRender="Calendar1_DayRender" OnSelectionChanged="Calendar1_SelectionChanged" BackColor="White" BorderColor="#3366CC" BorderWidth="1px" CellPadding="1" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="#003399" Height="193px">
                             <DayHeaderStyle BackColor="#99CCCC" ForeColor="#336666" Height="1px" />
                             <NextPrevStyle Font-Size="8pt" ForeColor="#CCCCFF" />
                             <OtherMonthDayStyle ForeColor="#999999" />
                             <SelectedDayStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                             <SelectorStyle BackColor="#99CCCC" ForeColor="#336666" />
                             <TitleStyle BackColor="#003399" BorderColor="#3366CC" BorderWidth="1px" Font-Bold="True" Font-Size="10pt" ForeColor="#CCCCFF" Height="25px" />
                             <TodayDayStyle BackColor="#99CCCC" ForeColor="White" />
                             <WeekendDayStyle BackColor="#CCCCFF" />
                         </asp:Calendar>
                         <br />
                         
                         
                         <asp:Label ID="Label1" runat="server" ForeColor="Red"></asp:Label>
                         <br /></td>
                </tr>
                 <tr>
                    <td class="auto-style13">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; To Date&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </td>
                     <td class="auto-style14"><asp:TextBox ID="txtToDate" runat="server"  placeholder="Enter ToDate"  Height="27px" Width="215px" CssClass="auto-style8"></asp:TextBox>
                         <asp:ImageButton ID="ImageButton2" runat="server" CssClass="auto-style17" Height="30px" ImageUrl="~/Images/download.png" OnClick="ImageButton2_Click" Width="29px" />
                         <br />
                         <asp:Calendar ID="Calendar2" runat="server" CssClass="auto-style1" Width="232px" OnDayRender="Calendar2_DayRender" OnSelectionChanged="Calendar2_SelectionChanged" BackColor="White" BorderColor="#3366CC" BorderWidth="1px" CellPadding="1" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="#003399" Height="193px">
                             <DayHeaderStyle BackColor="#99CCCC" ForeColor="#336666" Height="1px" />
                             <NextPrevStyle Font-Size="8pt" ForeColor="#CCCCFF" />
                             <OtherMonthDayStyle ForeColor="#999999" />
                             <SelectedDayStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                             <SelectorStyle BackColor="#99CCCC" ForeColor="#336666" />
                             <TitleStyle BackColor="#003399" BorderColor="#3366CC" BorderWidth="1px" Font-Bold="True" Font-Size="10pt" ForeColor="#CCCCFF" Height="25px" />
                             <TodayDayStyle BackColor="#99CCCC" ForeColor="White" />
                             <WeekendDayStyle BackColor="#CCCCFF" />
                         </asp:Calendar>
                         <br /></td>
                </tr>
                
                
                 <tr>
                    <td class="auto-style3">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Leave Type</td>
                     <td>
                         <asp:DropDownList ID="DropDownList1" runat="server" Height="27px" Width="114px" CssClass="auto-style5" >
                             <asp:ListItem>Half Day</asp:ListItem>
                             <asp:ListItem>Full Day</asp:ListItem>
                         </asp:DropDownList><br /><br /></td>
                </tr>
                 <tr>
                    <td class="auto-style15">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Leave Remarks&nbsp;&nbsp; </td>
                     <td class="auto-style16"><asp:TextBox ID="txtLeaveRemark" runat="server"  placeholder="Enter Remark" Height="30px" Width="233px" CssClass="auto-style5" ></asp:TextBox>
                         <br />
                         
                         
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtLeaveRemark" ErrorMessage="Enter Remarks First" ForeColor="Red"></asp:RequiredFieldValidator>
                         <br /></td>
                </tr>
            
                 </table>



        </div>
        <p>
            &nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnOK" runat="server" CssClass="auto-style4" Height="30px" OnClick="btnOK_Click" Text="OK" Width="82px" BackColor="#3399FF" ForeColor="White" />
        &nbsp;
            <asp:Button ID="btnExit" runat="server" BackColor="#0099FF" CssClass="auto-style1" ForeColor="White" Height="30px" OnClick="btnExit_Click" Text="Exit" Width="75px" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Dashboard.aspx">Go To Dashboard</asp:HyperLink>
        </p>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="3" CssClass="auto-style6" GridLines="None" BackColor="White" BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" CellSpacing="1">
            <Columns>
                <asp:BoundField DataField="ApplyDate" HeaderText="ApplyDate">
                <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="FromDate" HeaderText="FromDate">
                <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="ToDate" HeaderText="ToDate">
                <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="LeaveType" HeaderText="LeaveType">
                <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="ReasonForLeave" HeaderText="ReasonForLeave">
                <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
            </Columns>
            <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
            <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#E7E7FF" />
            <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
            <RowStyle BackColor="#DEDFDE" ForeColor="Black" />
            <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#594B9C" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#33276A" />
        </asp:GridView>
    </form>
</body>
</html>
