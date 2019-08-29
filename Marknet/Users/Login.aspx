<%@ Page Title="Marknet - כניסת משתמש רשום" Language="C#" MasterPageFile="~/Users/GeneralMaster.master"
    AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Users_Login" %>

<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <center>
        <asp:Login ID="memberLogin" runat="server" BackColor="" BorderColor="black" BorderPadding="4"
            BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="0.8em"
            ForeColor="#333333" OnAuthenticate="Auth" FailureText="!שם משתמש או סיסמה שגויים"
            LoginButtonText="כניסה" PasswordLabelText=":סיסמא" PasswordRequiredErrorMessage="הכנסת סיסמא נחוצה."
            RememberMeText=".זכור אותי" TextLayout="TextOnTop" TitleText="כניסת משתמש רשום"
            UserNameLabelText=":שם משתמש" UserNameRequiredErrorMessage=".הכנסת שם משתמש נחוץ"
            Height="175px" Width="199px">
            <InstructionTextStyle Font-Italic="True" ForeColor="Black" />
            <LayoutTemplate>
                <table dir="ltr" cellpadding="4" cellspacing="0" style="border-collapse: collapse;">
                    <tr>
                        <td>
                            <table cellpadding="0">
                                <tr>
                                    <td align="center" style="color: green; font-size: 16px; font-weight: bold;">
                                        כניסת משתמש רשום
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" style="color: black; font-size: 14px;">
                                        <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">:שם משתמש</asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" style="color: black; font-size: 14px;">
                                        <asp:TextBox ID="UserName" runat="server" Font-Size="13px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                                            ErrorMessage=".הכנסת שם משתמש נחוץ" ToolTip=".הכנסת שם משתמש נחוץ" ValidationGroup="memberLogin">*</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" style="color: black; font-size: 14px;">
                                        <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">:סיסמא</asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" style="color: black; font-size: 14px;">
                                        <asp:TextBox ID="Password" runat="server" Font-Size="14px" TextMode="Password"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                                            ErrorMessage="הכנסת סיסמא נחוצה." ToolTip="הכנסת סיסמא נחוצה." ValidationGroup="memberLogin">*</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" style="color: black; font-size: 12px;">
                                        <asp:CheckBox TextAlign="Left" ID="RememberMe" runat="server" Text=".זכור אותי" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" style="color: Red; font-size: 14px">
                                        <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:Panel DefaultButton="LoginButton">
                                            <asp:Button ID="LoginButton" runat="server" BackColor="White" BorderColor="#507CD1"
                                                BorderStyle="Solid" BorderWidth="1px" CommandName="Login" Font-Names="Verdana"
                                                Font-Size="14px" ForeColor="#284E98" Text="כניסה" ValidationGroup="memberLogin" />
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </LayoutTemplate>
            <LoginButtonStyle BackColor="White" BorderColor="#507CD1" BorderStyle="Solid" BorderWidth="1px"
                Font-Names="Verdana" Font-Size="0.8em" ForeColor="#284E98" />
            <TextBoxStyle Font-Size="0.8em" />
            <TitleTextStyle BackColor="#507CD1" Font-Bold="True" Font-Size="0.9em" ForeColor="White" />
        </asp:Login>
        <br />
        <asp:HyperLink ID="HyperLink5" runat="server" NavigateUrl="Register.aspx">רישום משתמש חדש</asp:HyperLink>
        <br />
        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="Forgot.aspx">שכחתי סיסמא</asp:HyperLink>
    </center>
</asp:Content>
