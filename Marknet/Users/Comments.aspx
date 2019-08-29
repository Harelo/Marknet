<%@ Page Title="Marknet - תגובות" Language="C#" MasterPageFile="~/Users/GeneralMaster.master"
    AutoEventWireup="true" CodeFile="Comments.aspx.cs" Inherits="Users_Comments" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <body dir="rtl">
        <asp:TextBox ID="CommentText" runat="server" Height="167px" Width="316px" dir="rtl"
            TextMode="MultiLine"></asp:TextBox>
        <br />
        <asp:Label ID="status" runat="server" ForeColor="Red"></asp:Label>
        <br />
        <asp:DropDownList ID="CommentType" runat="server" dir="rtl">
            <asp:ListItem>המלצה</asp:ListItem>
            <asp:ListItem>דווח</asp:ListItem>
            <asp:ListItem>תלונה</asp:ListItem>
            <asp:ListItem>אחר</asp:ListItem>
        </asp:DropDownList>
        &nbsp
        <asp:Label ID="Label1" runat="server" Text=":בחר סוג תגובה"></asp:Label>
        <br />
        <asp:Button ID="Comment" runat="server" Text="הגב" CssClass="commentButton" OnClick="Comment_Click" />
        <br />
        <asp:Label ID="note" runat="server" Text="' לא ניתן להשתמש בתו*"></asp:Label>
    </body>
</asp:Content>
