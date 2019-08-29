<%@ Page Title="Marknet - שכחתי סיסמא" Language="C#" MasterPageFile="~/Users/GeneralMaster.master" AutoEventWireup="true" CodeFile="Forgot.aspx.cs" Inherits="Users_Forgot" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <body dir="rtl">
        <style>
            table, td, th {
                text-align: center;
            }
        </style>
        <center>
        <table style="border: 1px solid black">
            <tr>
                <th>
                        <font color="green" size:"16px" style="font-family:Verdana"><b>:רשום את האימייל שלך או את שם המשתמש שלך</b></font>
                </th>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="status" runat="server" EnableTheming="True"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="userOrEmail" runat="server" EnableTheming="true"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="submit" runat="server" Text="שלח" OnClick="submit_Click" BackColor="White" BorderColor="#507CD1" BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="0.8em" ForeColor="#284E98" />
                </td>
            </tr>
        </table>
    </center>
    </body>
</asp:Content>

