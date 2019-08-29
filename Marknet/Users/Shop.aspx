<%@ Page Title="Marknet - החנות" Language="C#" MasterPageFile="~/Users/GeneralMaster.master"
    AutoEventWireup="true" CodeFile="Shop.aspx.cs" Inherits="Users_Shop" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:CheckBoxList ID="filters" runat="server" DataSourceID="AccessDataSource1" RepeatDirection="Horizontal"
        Style="height: 26px; width: 582px" DataValueField="CatagoryName" DataTextField="CatagoryName"
        AutoPostBack="true" OnSelectedIndexChanged="filters_SelectedIndexChanged" TextAlign="Left">
    </asp:CheckBoxList>
    <asp:AccessDataSource ID="AccessDataSource1" runat="server" DataFile="~/App_Data/Marknet.accdb"
        SelectCommand="SELECT * FROM [ProductCategories]"></asp:AccessDataSource>
        <br />
    <asp:TextBox ID="prodSrc" runat="server"></asp:TextBox>
    <asp:Button ID="src" runat="server" Text="חפש מוצר" OnClick="src_Click" 
        CssClass="generalButton" />
    <br />
    <asp:LinkButton ID="showAll" runat="server" onclick="showAll_Click">ראה הכל</asp:LinkButton>
    <br />
    <asp:Label ID="invalidAmount" runat="server" Text="!אין מספיק מוצרים במלאי" ForeColor="Red"
        Visible="False"></asp:Label>
    <br />
    <br />
    <asp:GridView ID="products" runat="server" AutoGenerateColumns="False" CellPadding="4"
        ForeColor="#333333" GridLines="None" AllowPaging="True" OnPageIndexChanging="products_PageIndexChanging"
        OnRowCommand="products_RowCommand" dir="rtl" 
        EmptyDataText="&lt;font color='red' size='4pt'&gt;המוצר שחיפשת לא נמצא!&lt;/font&gt;">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:BoundField DataField="ProductID" HeaderText="קוד מוצר" />
            <asp:BoundField DataField="ProductName" HeaderText="שם מוצר" />
            <asp:BoundField DataField="Stock" HeaderText="כמות מהמוצר" />
            <asp:BoundField DataField="Price" HeaderText="מחיר מוצר" />
            <asp:TemplateField HeaderText="תמונת מוצר">
                <ItemTemplate>
                    <asp:Image ID="Image1" runat="server" Height="100px" ImageUrl='<%# Bind("Picture") %>'
                        Width="100px" />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:ButtonField CommandName="addToBasket" Text="הוסף לסל" />
        </Columns>
        <EditRowStyle BackColor="#2461BF" />
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#EFF3FB" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#F5F7FB" />
        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
        <SortedDescendingCellStyle BackColor="#E9EBEF" />
        <SortedDescendingHeaderStyle BackColor="#4870BE" />
    </asp:GridView>
    </body>
    
</asp:Content>
