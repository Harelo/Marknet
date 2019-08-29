<%@ Page Title="Marknet Admins - צפייה בהזמנות" Language="C#" MasterPageFile="~/Admins/AdminsMaster.master"
    AutoEventWireup="true" CodeFile="Orders.aspx.cs" Inherits="Admins_Orders" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <center>
        <h2>
            צפייה בהזמנות</h2>
        <br />
        <asp:DropDownList ID="srcBy" runat="server" dir="rtl">
            <asp:ListItem>לפי שם לקוח</asp:ListItem>
            <asp:ListItem>לפי מספר הזמנה</asp:ListItem>
        </asp:DropDownList>
        <asp:TextBox ID="orderSrc" runat="server"></asp:TextBox>
        <asp:Button ID="src" runat="server" OnClick="src_Click" Text="חפש הזמנה" CssClass="generalButton" />
        <br />
        <asp:Label ID="status" runat="server" ForeColor="Red"></asp:Label>
        <br />
        <br />
        <asp:GridView ID="OrderDetails" runat="server" AutoGenerateColumns="False" BackColor="White"
            BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" CellPadding="4"
            GridLines="Horizontal">
            <Columns>
                <asp:TemplateField HeaderText="פרטי ההזמנה">
                    <ItemTemplate>
                        <asp:GridView ID="orderDetails" runat="server" AutoGenerateColumns="False" dir="rtl"
                            BackColor="White" BorderColor="#336666" BorderStyle="Double" BorderWidth="3px"
                            CellPadding="4" GridLines="Horizontal" OnRowDataBound="OrderDetailsRowDataBound"
                            ShowFooter="True">
                            <Columns>
                                <asp:BoundField HeaderText="שם מוצר" />
                                <asp:BoundField DataField="Amount" HeaderText="כמות" />
                                <asp:BoundField HeaderText="מחיר" />
                            </Columns>
                            <FooterStyle BackColor="White" ForeColor="#333333" />
                            <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="White" ForeColor="#333333" />
                            <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                            <SortedAscendingCellStyle BackColor="#F7F7F7" />
                            <SortedAscendingHeaderStyle BackColor="#487575" />
                            <SortedDescendingCellStyle BackColor="#E5E5E5" />
                            <SortedDescendingHeaderStyle BackColor="#275353" />
                        </asp:GridView>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="פרטי המזמין">
                    <ItemTemplate>
                        <asp:GridView ID="userDetails" runat="server" BackColor="White" dir="rtl" BorderColor="#336666"
                            BorderStyle="Double" BorderWidth="3px" CellPadding="4" AutoGenerateColumns="False"
                            GridLines="Horizontal" OnRowDataBound="UserDetailsRowDataBound">
                            <Columns>
                                <asp:BoundField DataField="ID" HeaderText="קוד משתמש" ReadOnly="True">
                                    <ControlStyle Width="60px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Username" HeaderText="שם משתמש">
                                    <ControlStyle Width="60px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="UserPassword" HeaderText="סיסמא">
                                    <ControlStyle Width="60px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Age" HeaderText="גיל">
                                    <ControlStyle Width="60px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Gender" HeaderText="מין">
                                    <ControlStyle Width="60px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="FullName" HeaderText="שם מלא">
                                    <ControlStyle Width="60px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Email" HeaderText="אימייל">
                                    <ControlStyle Width="160px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Role" HeaderText="תפקיד">
                                    <ControlStyle Width="60px" />
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
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <FooterStyle BackColor="White" ForeColor="#333333" />
            <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="White" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F7F7F7" />
            <SortedAscendingHeaderStyle BackColor="#487575" />
            <SortedDescendingCellStyle BackColor="#E5E5E5" />
            <SortedDescendingHeaderStyle BackColor="#275353" />
        </asp:GridView>
        <br />
    </center>
</asp:Content>
