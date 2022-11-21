﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HolidayMaster1.aspx.cs" Inherits="LoginLogoutPanelProject.HolidayMaster1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">

        .auto-style5 {
            margin-left: 618px;
            margin-top: 21px;
        }
        .auto-style2 {
            height: 49px;
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
          
               <h1 style="text-align: center">Holiday </h1>

                
                 <tr>
                    <td class="auto-style11">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Date of Holiday</td>
                     <td class="auto-style12"><asp:TextBox ID="txtDate" runat="server"  placeholder="Enter Date"  Height="31px" Width="231px" CssClass="auto-style7" ></asp:TextBox>
                         <asp:ImageButton ID="ImageButton1" runat="server" CssClass="auto-style18" Height="29px" ImageUrl="~/Images/download.png" OnClick="ImageButton1_Click" Width="33px" style="margin-left: 6px" />
                         <asp:Calendar ID="Calendar1" runat="server" CssClass="auto-style1" Width="234px" OnDayRender="Calendar1_DayRender" OnSelectionChanged="Calendar1_SelectionChanged" BackColor="White" BorderColor="#3366CC" BorderWidth="1px" CellPadding="1" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="#003399" Height="196px">
                             <DayHeaderStyle BackColor="#99CCCC" ForeColor="#336666" Height="1px" />
                             <NextPrevStyle Font-Size="8pt" ForeColor="#CCCCFF" />
                             <OtherMonthDayStyle ForeColor="#999999" />
                             <SelectedDayStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                             <SelectorStyle BackColor="#99CCCC" ForeColor="#336666" />
                             <TitleStyle BackColor="#003399" BorderColor="#3366CC" BorderWidth="1px" Font-Bold="True" Font-Size="10pt" ForeColor="#CCCCFF" Height="25px" />
                             <TodayDayStyle BackColor="#99CCCC" ForeColor="White" />
                             <WeekendDayStyle BackColor="#CCCCFF" />
                         </asp:Calendar>
                         <asp:Label ID="Label1" runat="server" ForeColor="Red"></asp:Label>
                         <br />
                         
                         
                         <br /></td>
                </tr>
                 <tr>
                    <td class="auto-style13">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Reason for Holiday&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </td>
                     <td class="auto-style14"><asp:TextBox ID="txtReason" runat="server"  placeholder="Enter Reason"  Height="32px" Width="234px" CssClass="auto-style8"></asp:TextBox>
                         <br />
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtReason" ErrorMessage="Enter Remarks First" ForeColor="Red"></asp:RequiredFieldValidator>
                         <br /></td>
                </tr>
                
                
                 
                 
            
                 </table>



        </div>
        <p>
            &nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnOK" runat="server" CssClass="auto-style4" Height="30px" OnClick="btnOK_Click" Text="OK" Width="82px" BackColor="#3399FF" ForeColor="White" style="margin-left: 732px" />
        &nbsp;
            <asp:Button ID="btnExit" runat="server" BackColor="#0099FF" CssClass="auto-style1" ForeColor="White" Height="30px" OnClick="btnExit_Click" Text="Exit" Width="75px" />
        &nbsp;&nbsp;&nbsp;
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Dashboard.aspx">Go To Dashboard</asp:HyperLink>
        </p>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" AutoGenerateDeleteButton="True" AutoGenerateSelectButton="True" CellPadding="3" CssClass="auto-style5" Height="142px" OnRowDeleting="GridView1_RowDeleting"  OnSelectedIndexChanging="GridView1_SelectedIndexChanging" Width="326px" GridLines="None" BackColor="White" BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" CellSpacing="1">
            <Columns>
                <asp:BoundField DataField="DateOfHoliday" HeaderText="DateOfHoliday">
                <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="Reasonforholiday" HeaderText="Reasonforholiday">
                <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
            </Columns>
            <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
            <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#E7E7FF" />
            <PagerStyle ForeColor="Black" HorizontalAlign="Right" BackColor="#C6C3C6" />
            <RowStyle BackColor="#DEDFDE" ForeColor="Black" />
            <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#594B9C" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#33276A" />
        </asp:GridView>
        </div>
    </form>
</body>
</html>
