<%@ Page Title="Marknet - סיום הזמנה" Language="C#" MasterPageFile="~/Users/GeneralMaster.master"
    AutoEventWireup="true" CodeFile="FinishOrder.aspx.cs" Inherits="Users_FinishOrder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:GridView ID="Basket" dir="rtl" runat="server" AutoGenerateColumns="False" CellPadding="4"
        ForeColor="#333333" GridLines="None" ShowFooter="True" OnRowDataBound="Basket_RowDataBound">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
            <asp:BoundField DataField="ProductID" HeaderText="קוד מוצר" />
            <asp:BoundField DataField="ProductName" HeaderText="שם מוצר" />
            <asp:BoundField DataField="Stock" HeaderText="כמות במלאי" />
            <asp:BoundField DataField="AmountRequired" HeaderText="כמות מבוקשת" />
            <asp:BoundField DataField="Price" HeaderText="מחיר מוצר" />
        </Columns>
        <EditRowStyle BackColor="#999999" />
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#E9E7E2" />
        <SortedAscendingHeaderStyle BackColor="#506C8C" />
        <SortedDescendingCellStyle BackColor="#FFFDF8" />
        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
    </asp:GridView>
    <asp:Label ID="status" runat="server" Visible=false Font-Bold="False" ForeColor="Red" 
        Text=".שגיאה בביצוע התשלום באמצעות כרטיס האשראי שסופק"></asp:Label>
    <br />
    <br />
    <table dir="rtl" align="center" style="border: 1px solid black">
<font color="green" size:"16px" style="font-family:Verdana"><b>פרטי כרטיס אשראי</b></font>
        <tr>
            <td>
                מספר כרטיס אשראי:
            </td>
            <td>
                <asp:TextBox ID="CardNumber" runat="server" Width="130px" 
                    onkeypress="return isNumberKey(event)" MaxLength="16"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                3 ספרות מאחורי הכרטיס:
            </td>
            <td>
                <asp:TextBox ID="CSC" runat="server" Width="40px" 
                    onkeypress="return isNumberKey(event)" MaxLength="3"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                תוקף (חודש):
            </td>
            <td>
                <asp:TextBox ID="CardMonth" runat="server" Width="30px" 
                    onkeypress="return isNumberKey(event)" MaxLength="2"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                תוקף (שנה):
            </td>
            <td>
                <asp:TextBox ID="CardYear" runat="server" Width="30px" 
                    onkeypress="return isNumberKey(event)" MaxLength="2"></asp:TextBox>
            </td>
        </tr>
        </table>
        <br />
        <asp:Button ID="payNow" runat="server" CssClass="payNowButton" Text="שלם עכשיו" OnClick="payNow_Click" />

        <script language="javascript">

        function isNumberKey(evt)
      {
         var charCode = (evt.which) ? evt.which : event.keyCode
         if (charCode > 31 && (charCode < 48 || charCode > 57))
            return false;

         return true;
      }
        </script>
</asp:Content>
