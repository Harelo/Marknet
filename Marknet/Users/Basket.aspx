<%@ Page Title="" Language="C#" MasterPageFile="~/Users/GeneralMaster.master" AutoEventWireup="true"
    CodeFile="Basket.aspx.cs" Inherits="Users_Basket" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Marknet - סל הקניות שלך</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td dir="rtl">
                <asp:GridView ID="Basket" runat="server" AutoGenerateColumns="False" CellPadding="4"
                    ForeColor="#333333" GridLines="None" OnRowDeleting="Basket_RowDeleting" ShowFooter="True"
                    OnRowDataBound="Basket_RowDataBound" 
                    EmptyDataText="&lt;font color='red' size='4pt'&gt;הסל שלך ריק!&lt;/font&gt;" OnRowCancelingEdit="Basket_RowCancelingEdit"
                    OnRowEditing="Basket_RowEditing" OnRowUpdating="Basket_RowUpdating">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:CommandField CancelText="בטל" DeleteText="מחק" EditText="שנה כמות" InsertText="הכנס"
                            NewText="חדש" SelectText="בחר" ShowEditButton="True" UpdateText="עדכן" />
                        <asp:BoundField DataField="ProductID" HeaderText="קוד מוצר" ReadOnly="True" />
                        <asp:BoundField DataField="ProductName" HeaderText="שם מוצר" ReadOnly="True" />
                        <asp:BoundField DataField="Stock" HeaderText="כמות במלאי" ReadOnly="True" />
                        <asp:BoundField DataField="Price" HeaderText="מחיר" ReadOnly="True" />
                        <asp:BoundField DataField="AmountRequired" HeaderText="כמות מבוקשת">
                            <ControlStyle Width="30px" />
                        </asp:BoundField>
                        <asp:CommandField HeaderText="הסר מהסל" ShowDeleteButton="True" DeleteText="הסר מהסל" />
                    </Columns>
                    <EditRowStyle BackColor="#7C6F57" />
                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#E3EAEB" />
                    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#F8FAFA" />
                    <SortedAscendingHeaderStyle BackColor="#246B61" />
                    <SortedDescendingCellStyle BackColor="#D4DFE1" />
                    <SortedDescendingHeaderStyle BackColor="#15524A" />
                </asp:GridView>
            </td>
        </tr>
    </table>
    <asp:CustomValidator ID="loggedInValidation" runat="server" ErrorMessage="!אינך מחובר. אנא התחבר כדי לסיים את ההזמנה"
        Font-Bold="False" ForeColor="Red"></asp:CustomValidator>
    <br />
    <asp:Label ID="status" runat="server" Text="!אין מספיק מוצרים במלאי. אנא בחר כמות אחרת"
        ForeColor="Red" Visible="False"></asp:Label>
    <br />
    <asp:Button ID="finishOrder" CssClass="finishOrderButton" runat="server" OnClick="finishOrder_Click"
        Text="סיים הזמנה" Visible="False" />
</asp:Content>
