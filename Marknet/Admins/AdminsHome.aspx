<%@ Page Title="Marknet Admins - עמוד ראשי" Language="C#" MasterPageFile="~/Admins/AdminsMaster.master"
    AutoEventWireup="true" CodeFile="AdminsHome.aspx.cs" Inherits="Admins_AdminsHome" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <center>
        <h2>
            :אנא בחר את הפעולה אותה ברצונך לבצע</h2>
        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="UpdateProducts.aspx" CssClass="adminsMenuLink">צפייה במוצרים</asp:HyperLink>
        <br />
        <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="UpdateUsers.aspx" CssClass="adminsMenuLink">צפייה במשתמשים</asp:HyperLink>
        <br />
        <asp:HyperLink ID="HyperLink5" runat="server" NavigateUrl="Orders.aspx" CssClass="adminsMenuLink">צפייה בהזמנות</asp:HyperLink>
        <br />
        <asp:HyperLink ID="HyperLink6" runat="server" NavigateUrl="ReadComments.aspx" CssClass="adminsMenuLink">צפייה בתגובות</asp:HyperLink>
        <br />
        <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="Statistics.aspx" CssClass="adminsMenuLink">סטטיסטיקות</asp:HyperLink>
        <br />
        <br />
        <asp:HyperLink ID="HyperLink3" NavigateUrl="~/Users/Home.aspx" runat="server">חזרה לדף הבית</asp:HyperLink>
        <link href="AdminsCss.css" rel="stylesheet" type="text/css" />
    </center>
</asp:Content>
