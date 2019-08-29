<%@ Page Title="Marknet Admins - סטטיסטיקות" Language="C#" MasterPageFile="~/Admins/AdminsMaster.master"
    AutoEventWireup="true" CodeFile="Statistics.aspx.cs" Inherits="Admins_Statistics" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <center style="margin-left: 40px">
        <h2>
            סטטיסטיקות</h2>
        <p dir="rtl">
            <table align='center' border='1px' cellspacing='0' style='text-align: center; direction: right;
                border: solid 1px Black; font-size: medium;'>
                <tr>
                    <td>
                        <b>
                            <h3>
                                משתמשים</h3>
                        </b>
                        <asp:Label dir="rtl" ID="stat1" runat="server"></asp:Label>
                        <br />
                        <asp:Label dir="rtl" ID="stat2" runat="server" Text=""></asp:Label>
                        <br />
                        <asp:Label dir="rtl" ID="stat3" runat="server" Text=""></asp:Label>
                        <br />
                        <asp:Label dir="rtl" ID="stat4" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>
                            <h3>
                                מוצרים</h3>
                        </b>
                        <asp:Label dir="rtl" ID="stat5" runat="server" Text=""></asp:Label>
                        <br />
                        <asp:Label dir="rtl" ID="stat6" runat="server" Text=""></asp:Label>
                        <br />
                        <asp:Label dir="rtl" ID="stat7" runat="server" Text=""></asp:Label>
                        <br />
                        <asp:Label dir="rtl" ID="stat8" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>
                            <h3>
                                הזמנות</h3>
                        </b>
                        <asp:Label dir="rtl" ID="stat9" runat="server" Text=""></asp:Label>
                        <br />
                        <asp:Label dir="rtl" ID="stat10" runat="server" Text=""></asp:Label>
                        <br />
                        <asp:Label dir="rtl" ID="stat11" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>
                            <h3>
                                תגובות</h3>
                        </b>
                        <asp:Label dir="rtl" ID="stat12" runat="server" Text=""></asp:Label>
                        <br />
                        <asp:DropDownList ID="CommentTypes" runat="server" dir="rtl" DataSourceID="CommentTypesSource"
                            DataTextField="CatagoryName" DataValueField="CatagoryName" 
                            AutoPostBack="True" onselectedindexchanged="CommentTypes_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:AccessDataSource ID="CommentTypesSource" runat="server" DataFile="~/App_Data/Marknet.accdb"
                            SelectCommand="SELECT [CatagoryName] FROM [CommentCatagories]"></asp:AccessDataSource>
                        <asp:Label dir="rtl" ID="stat13" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
            </table>
        </p>
    </center>
</asp:Content>
