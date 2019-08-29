<%@ Page Title="Marknet Admins - עדכון טבלת מוצרים" Language="C#" MasterPageFile="~/Admins/AdminsMaster.master"
    AutoEventWireup="true" CodeFile="UpdateProducts.aspx.cs" Inherits="Admins_UpdateProducts" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <center style="direction: ltr">
        <h2>
            צפייה במוצרים</h2>
        <asp:TextBox ID="productSrc" runat="server"></asp:TextBox>
        <asp:Button ID="src" runat="server" OnClick="src_Click" Text="חפש מוצר" CssClass="generalButton" />
        <br />
        <asp:LinkButton ID="showAll" runat="server" OnClick="showAll_Click">ראה הכל</asp:LinkButton>
        <br />
        <br />
        <asp:GridView ID="productsTbl" runat="server" AutoGenerateColumns="False" dir="rtl"
            BackColor="White" BorderColor="#336666" BorderStyle="Double" BorderWidth="3px"
            CellPadding="4" GridLines="Horizontal" OnRowUpdating="productsTbl_RowUpdating"
            Height="151px" OnRowCancelingEdit="productsTbl_RowCancelingEdit" OnRowEditing="productsTbl_RowEditing"
            AllowPaging="True" OnPageIndexChanging="productsTbl_PageIndexChanging" OnRowDeleting="productsTbl_RowDeleting">
            <Columns>
                <asp:CommandField CancelText="בטל" DeleteText="מחק" EditText="ערוך" InsertText="אשר"
                    SelectText="בחר" ShowEditButton="True" UpdateText="עדכן" ShowDeleteButton="True" />
                <asp:BoundField DataField="ProductID" HeaderText="קוד מוצר" ReadOnly="True" />
                <asp:BoundField DataField="ProductName" HeaderText="שם מוצר">
                    <ControlStyle Width="60px" />
                </asp:BoundField>
                <asp:BoundField DataField="Price" HeaderText="מחיר">
                    <ControlStyle Width="30px" />
                </asp:BoundField>
                <asp:BoundField DataField="Stock" HeaderText="כמות מהמוצר">
                    <ControlStyle Width="30px" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="תמונת מוצר">
                    <ItemTemplate>
                        <asp:Image ID="Image1" runat="server" Height="42px" ImageUrl='<%# Bind("Picture") %>'
                            Width="48px" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="קוד קטגוריה">
                    <EditItemTemplate>
                        <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="AccessDataSource1"
                            DataTextField="CatagoryName" DataValueField="CatagoryID">
                        </asp:DropDownList>
                        <asp:AccessDataSource ID="AccessDataSource1" runat="server" DataFile="~/App_Data/Marknet.accdb"
                            SelectCommand="SELECT [CatagoryID], [CatagoryName] FROM [ProductCategories]">
                        </asp:AccessDataSource>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("CatagoryID") %>'></asp:Label>
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
        <asp:Label ID="uploadErr" runat="server"></asp:Label>
        <br />
                <p dir="rtl" align="center">
            <asp:AccessDataSource ID="AccessDataSource2" runat="server" 
                DataFile="~/App_Data/Marknet.accdb" 
                SelectCommand="SELECT [CatagoryName] FROM [ProductCategories]">
            </asp:AccessDataSource>
            <asp:LinkButton ID="Add" runat="server" onclick="Add_Click">הוסף מוצר חדש</asp:LinkButton>&nbsp
            <asp:LinkButton ID="Cancel" runat="server" Visible=false onclick="Cancel_Click">בטל</asp:LinkButton>&nbsp
            <asp:TextBox ID="NewName" runat="server" onfocus="javascript:if(this.value == 'שם מוצר') { this.value=''; this.style.color='black' }"
                
                        onblur="javascript:if(this.value == '') { this.value='שם מוצר'; this.style.color='gray' }" Visible=false
                ForeColor="Gray" Width="120px">שם מוצר</asp:TextBox>
            <asp:TextBox ID="NewPrice" runat="server" onfocus="javascript:if(this.value == 'מחיר מוצר') { this.value=''; this.style.color='black' }"
                onblur="javascript:if(this.value == '') { this.value='מחיר מוצר'; this.style.color='gray' }"
                ForeColor="Gray" Visible="False" Width="60px">מחיר מוצר</asp:TextBox>
            <asp:TextBox ID="NewStock" runat="server" onfocus="javascript:if(this.value == 'כמות מהמוצר') { this.value=''; this.style.color='black' }"
                onblur="javascript:if(this.value == '') { this.value='כמות מהמוצר'; this.style.color='gray' }"
                ForeColor="Gray" Visible="False" Width="80px">כמות מהמוצר</asp:TextBox>
                <asp:DropDownList ID="NewCatagory" runat="server" 
                DataSourceID="AccessDataSource2" DataTextField="CatagoryName" 
                DataValueField="CatagoryName" Visible="False"></asp:DropDownList>
            <br />
        </p>
        <asp:FileUpload ID="fileUpload" runat="server" Visible="False" accept="image/*" />
        <br />
        <br />
        </div>
    </center>
</asp:Content>
