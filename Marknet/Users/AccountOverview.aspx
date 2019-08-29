<%@ Page Title="Marknet - עדכון פרטי משתמש" Language="C#" MasterPageFile="~/Users/GeneralMaster.master"
    AutoEventWireup="true" CodeFile="AccountOverview.aspx.cs" Inherits="Users_AccountOverview" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h3>
        <font color="green">:פרטי המשתמש שלך הם</font></h3>
    <asp:GridView ID="accountOverview" runat="server" ShowHeader="False" dir="rtl" CellPadding="4"
        ForeColor="#333333" GridLines="None" OnRowCancelingEdit="accountOverview_RowCancelingEdit"
        OnRowEditing="accountOverview_RowEditing" OnRowUpdating="accountOverview_RowUpdating"
        AutoGenerateColumns="False">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:CommandField CancelText="בטל" DeleteText="מחק" EditText="ערוך" InsertText="הכנס"
                NewText="חדש" SelectText="בחר" ShowEditButton="True" UpdateText="עדכן" />
            <asp:BoundField DataField="Type" ReadOnly="True" />
            <asp:BoundField DataField="Info"></asp:BoundField>
        </Columns>
        <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
        <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
        <SortedAscendingCellStyle BackColor="#FDF5AC" />
        <SortedAscendingHeaderStyle BackColor="#4D0000" />
        <SortedDescendingCellStyle BackColor="#FCF6C0" />
        <SortedDescendingHeaderStyle BackColor="#820000" />
    </asp:GridView>
    <br />
    <asp:AccessDataSource ID="AccessDataSource1" runat="server" DataFile="~/App_Data/Marknet.accdb"
        SelectCommand="SELECT [CityName] FROM [Cities]"></asp:AccessDataSource>
    <asp:Label ID="error" runat="server" Text=".שם המשתמש שבחרת כבר קיים במערכת. אנא בחר שם אחר"
        Visible="False" ForeColor="Red"></asp:Label>
    <script language="javascript">
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;


            return true;
        }

        function clipboardContainsLetters(evt) {
            var data = evt.clipboardData.getData('text/plain');
            if (data.match(/[a-z]/i)) {
                return false;
            }
            return true;
        }

    </script>
</asp:Content>
