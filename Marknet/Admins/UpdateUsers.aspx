<%@ Page Title="Marknet Admins - עדכון טבלת משתמשים" Language="C#" MasterPageFile="~/Admins/AdminsMaster.master" AutoEventWireup="true"
    CodeFile="UpdateUsers.aspx.cs" Inherits="Admins_UpdateUsers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <center style="direction: rtl">
            <h2>
            צפייה במשתמשים</h2>
        <asp:Button ID="src" runat="server" OnClick="src_Click" Text="חפש משתמש" 
            CssClass="generalButton" />
        <asp:TextBox ID="userSrc" runat="server"></asp:TextBox>
        <br />
        <asp:LinkButton ID="showAll" runat="server" OnClick="showAll_Click">ראה הכל</asp:LinkButton>
        <br />
            <asp:Label ID="memberAmount" runat="server" Text="Label"></asp:Label>
        <br />
        <asp:GridView ID="usersTbl" runat="server" BackColor="White" dir="rtl"
            BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" CellPadding="4" 
            AutoGenerateColumns="False" onrowcancelingedit="usersTbl_RowCancelingEdit" 
            onrowdeleting="usersTbl_RowDeleting" onrowediting="usersTbl_RowEditing" 
            onrowupdating="usersTbl_RowUpdating" AllowPaging="True" 
                onpageindexchanging="usersTbl_PageIndexChanging" GridLines="Horizontal" 
                Height="151px" onrowdatabound="usersTbl_RowDataBound" 
                ondatabound="usersTbl_DataBound">
            <Columns>
                <asp:CommandField CancelText="בטל" DeleteText="מחק" EditText="ערוך" 
                    InsertText="עדכן" SelectText="בחר" ShowDeleteButton="True" 
                    ShowEditButton="True" UpdateText="עדכן" />
                <asp:BoundField DataField="ID" HeaderText="קוד משתמש" ReadOnly="True" >
                <ControlStyle Width="60px" />
                </asp:BoundField>
                <asp:BoundField DataField="Username" HeaderText="שם משתמש" >
                <ControlStyle Width="60px" />
                </asp:BoundField>
                <asp:BoundField DataField="UserPassword" HeaderText="סיסמא" >
                <ControlStyle Width="120px" />
                </asp:BoundField>
                <asp:BoundField DataField="Age" HeaderText="גיל" >
                <ControlStyle Width="30px" />
                </asp:BoundField>
                <asp:BoundField DataField="Gender" HeaderText="מין" >
                <ControlStyle Width="60px" />
                </asp:BoundField>
                <asp:BoundField DataField="FullName" HeaderText="שם מלא" >
                <ControlStyle Width="60px" />
                </asp:BoundField>
                <asp:BoundField DataField="Email" HeaderText="אימייל" >
                <ControlStyle Width="160px" />
                </asp:BoundField>
                <asp:BoundField DataField="Role" HeaderText="תפקיד" >
                <ControlStyle Width="60px" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="עיר מגורים">
                    <EditItemTemplate>
                        <asp:DropDownList ID="DropDownList1" runat="server" 
                            DataSourceID="AccessDataSource1" DataTextField="CityName" 
                            DataValueField="CityName">
                        </asp:DropDownList>
                        <asp:AccessDataSource ID="AccessDataSource1" runat="server" 
                            DataFile="~/App_Data/Marknet.accdb" 
                            SelectCommand="SELECT [CityName] FROM [Cities]"></asp:AccessDataSource>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("City") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Address" HeaderText="כתובת">
                <ControlStyle Width="60px" />
                </asp:BoundField>
                <asp:BoundField DataField="ApartmentNumber" HeaderText="מספר דירה">
                <ControlStyle Width="30px" />
                </asp:BoundField>
            </Columns>
            <FooterStyle BackColor="White" ForeColor="#333333" />
            <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle ForeColor="#333333" BackColor="White" />
            <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F7F7F7" />
            <SortedAscendingHeaderStyle BackColor="#487575" />
            <SortedDescendingCellStyle BackColor="#E5E5E5" />
            <SortedDescendingHeaderStyle BackColor="#275353" />
        </asp:GridView>
    </center>
</asp:Content>
