<%@ Page Title="Marknet Admins - צפייה בתגובות" Language="C#" MasterPageFile="~/Admins/AdminsMaster.master" AutoEventWireup="true"
    CodeFile="ReadComments.aspx.cs" Inherits="Admins_ReadComments" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <center>
        <body>
        <h2>צפייה בתגובות</h2>
            <asp:GridView ID="Comments" dir="rtl" runat="server" AutoGenerateColumns="False"
                BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px"
                CellPadding="4" ForeColor="Black" GridLines="Vertical" 
                OnRowDataBound="Comments_RowDataBound" AllowPaging="True" 
                onpageindexchanging="Comments_PageIndexChanging" PageSize="5">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:BoundField DataField="CommentID" HeaderText="מזהה תגובה" />
                    <asp:BoundField DataField="UserID" HeaderText="שם המגיב" />
                    <asp:BoundField DataField="CommentDate" HeaderText="תאריך התגובה" />
                    <asp:BoundField DataField="CommentType" HeaderText="סוג התגובה" />
                    <asp:BoundField DataField="CommentText" HeaderText="תוכן התגובה" >
                    <ControlStyle Height="1000px" Width="100px" />
                    <ItemStyle Height="150px" Width="200px" />
                    </asp:BoundField>
                </Columns>
                <FooterStyle BackColor="#CCCC99" />
                <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                <RowStyle BackColor="#F7F7DE" />
                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#FBFBF2" />
                <SortedAscendingHeaderStyle BackColor="#848384" />
                <SortedDescendingCellStyle BackColor="#EAEAD3" />
                <SortedDescendingHeaderStyle BackColor="#575357" />
            </asp:GridView>
        </body>
    </center>
</asp:Content>
