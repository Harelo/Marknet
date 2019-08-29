<%@ Page Language="C#" Title="Marknet - רישום משתמש חדש" AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="_Default" MasterPageFile="~/Users/GeneralMaster.master" %>

<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <center style="margin-left: 40px">
        <style>
            table, td, th {
                text-align: center;
            }
            .auto-style1 {
                height: 28px;
            }
            .style1
            {
                height: 30px;
            }
        </style>
        <table dir="rtl" align="center" style="border: 1px solid black">
            <tr>
                <th>
                        <font color="green" size:"16px" style="font-family:Verdana"><b>רישום משתמש חדש</b></font></th>
            </tr>
            <tr>
                <td class="auto-style1">
                                            <asp:Label ID="status" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="שם משתמש: "></asp:Label>
                    <asp:TextBox ID="user" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="userChecker" runat="server" ControlToValidate="user" ErrorMessage="*" ValidationGroup="registerGroup"></asp:RequiredFieldValidator>
                </td>
            </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label3" runat="server" Text="סיסמא:"></asp:Label>
                        <asp:TextBox ID="pass" runat="server" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="passChecker" runat="server" ControlToValidate="pass" ErrorMessage="*" ValidationGroup="registerGroup"></asp:RequiredFieldValidator>
                </tr>
                </td>
                <tr>
                    <td class="style1">
                        <asp:Label ID="Label6" runat="server" Text="חזור על הסיסמא:"></asp:Label>
                        <asp:TextBox ID="confirmPass" runat="server" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="confirmpassChecker" runat="server" ControlToValidate="confirmPass" ErrorMessage="*" ValidationGroup="registerGroup"></asp:RequiredFieldValidator>
                </tr>
                </td>
                <tr>
                    <td>
                        <asp:Label ID="Label2" runat="server" Text="גיל: "></asp:Label>
                        <asp:TextBox ID="age" runat="server" onkeypress="return isNumberKey(event)"
                            MaxLength="3"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="ageChecker" runat="server" ControlToValidate="age" ErrorMessage="*" ValidationGroup="registerGroup"></asp:RequiredFieldValidator>
                </tr>
                </td>
                            <tr>
                <td>
                    <asp:Label ID="Label7" runat="server" Text="מין: "></asp:Label>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ControlToValidate="Gender" ErrorMessage="*" ValidationGroup="registerGroup"></asp:RequiredFieldValidator>
                    <asp:RadioButtonList ID="Gender" runat="server" RepeatDirection="Horizontal" 
                        RepeatLayout="Flow">
                        <asp:ListItem>זכר</asp:ListItem>
                        <asp:ListItem>נקבה</asp:ListItem>
                    </asp:RadioButtonList>
                    </tr>
                </td>
                <tr>
                    <td>
                        <asp:Label ID="Label4" runat="server" Text="שם מלא: "></asp:Label>
                        <asp:TextBox ID="fullName" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="fullNameChecker" runat="server" ControlToValidate="fullName" ErrorMessage="*" ValidationGroup="registerGroup"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label5" runat="server" Text="אימייל: "></asp:Label>
                        <asp:TextBox ID="email" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="emailChecker" runat="server" ControlToValidate="email" ErrorMessage="*" ValidationGroup="registerGroup"></asp:RequiredFieldValidator>
                </tr>
                </td>
                <tr>
                <td>
                <asp:Label ID="Label16" runat="server" Text="עיר: "></asp:Label>
                              <asp:DropDownList ID="City" runat="server" 
                        DataSourceID="AccessDataSource1" DataTextField="CityName" 
                        DataValueField="CityName">
                              </asp:DropDownList>
                    <asp:AccessDataSource ID="AccessDataSource1" runat="server" 
                        DataFile="~/App_Data/Marknet.accdb" 
                        SelectCommand="SELECT [CityName] FROM [Cities]"></asp:AccessDataSource>
                </td>
                </tr>
                                <tr>
                <td>
                <asp:Label ID="Label8" runat="server" Text="כתובת: "></asp:Label>
                        <asp:TextBox ID="address" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="addressChecker" runat="server" ControlToValidate="address" ErrorMessage="*" ValidationGroup="registerGroup"></asp:RequiredFieldValidator>
                </td>
                </tr>
                                                <tr>
                <td>
                <asp:Label ID="Label9" runat="server" Text="מספר דירה: " ></asp:Label>
                        <asp:TextBox ID="apartmentNum" runat="server" 
                        onkeypress="return isNumberKey(event)" MaxLength="3"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="apartmentNumChecker" runat="server" ControlToValidate="apartmentNum" ErrorMessage="*" ValidationGroup="registerGroup"></asp:RequiredFieldValidator>
                </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="register" runat="server" Text="הרשם" OnClick="register_Click" CausesValidation="true" ValidationGroup="registerGroup" BackColor="White" BorderColor="#507CD1" BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="0.8em" ForeColor="#284E98"/>
                    </td>
                </tr>
        </table>
    </center>
                        <asp:RegularExpressionValidator ID="passwordValidator" 
        runat="server" ControlToValidate="pass" Display="None" 
        ErrorMessage="סיסמא אינה חוקית. הסיסמא חייבת להכיל 6 תווים לפחות." 
        ValidationExpression="\w{6,}" ValidationGroup="registerGrou["></asp:RegularExpressionValidator>
                        <asp:RegularExpressionValidator ID="ageValidator" runat="server" 
                        ControlToValidate="age" ErrorMessage="גיל אינו חוקי." 
                        ValidationExpression="\d{1,2}" 
        ValidationGroup="registerGroup" Display="None"></asp:RegularExpressionValidator>
                        <asp:CompareValidator ID="confirmPassValidator" runat="server" 
                        ControlToCompare="pass" ControlToValidate="confirmPass" 
                        CausesValidation="false" Display="None" ErrorMessage="הסיסמאות אינן תואמות." 
                        ValidationGroup="registerGroup"></asp:CompareValidator>
                     <asp:CustomValidator ID="invalidEmail" runat="server" ValidationGroup="registerGroup"
                        ErrorMessage="האימייל שבחרת כבר קיים במערכת. אנא בחר אימייל אחר." 
                        Display="None" onservervalidate="invalidEmail_ServerValidate"></asp:CustomValidator>
                    <asp:CustomValidator ID="invalidUsername" runat="server" ValidationGroup="registerGroup"
                        ErrorMessage="שם המשתמש שבחרת כבר קיים במערכת. אנא בחר שם משתמש אחר." 
                        Display="None" onservervalidate="invalidUsername_ServerValidate"></asp:CustomValidator>
    </body>
    <asp:RegularExpressionValidator ID="emailValidator" runat="server" 
        ControlToValidate="email" Display="None" ErrorMessage="האימייל אינו תקין." 
        ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
        ValidationGroup="registerGroup"></asp:RegularExpressionValidator>
    <asp:RegularExpressionValidator ID="nameValidator" runat="server" 
        ControlToValidate="fullName" Display="None" ErrorMessage="שם אינו תקין." 
        ValidationExpression="\w+[ ]\w+"></asp:RegularExpressionValidator>

        <script language=javascript>
            function isNumberKey(evt) {
                var charCode = (evt.which) ? evt.which : event.keyCode
                if (charCode > 31 && (charCode < 48 || charCode > 57))
                    return false;

                return true;
            }
        </script>
</asp:Content>
