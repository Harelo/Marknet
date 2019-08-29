using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text.RegularExpressions;
using System.Drawing;

public partial class Users_AccountOverview : System.Web.UI.Page
{
    private static string Username;
    private static string TheCityName;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (accountOverview.EditIndex == 6)
        {
            AddCitiesControl(TheCityName);
        }

        if (!IsPostBack)
        {
            if (Session["Username"] != null)
            {
                Username = Session["Username"].ToString();
                UpdateAccountOverview();
            }
            else
                Response.Redirect("Login.aspx");
        }
    }

    protected void UpdateAccountOverview()
    {
        accountOverview.DataSource = createCustomTable();
        accountOverview.DataBind();
        for (int i = 0; i < accountOverview.Rows.Count; i++)
            accountOverview.Rows[i].Cells[1].Font.Bold = true;
        accountOverview.Rows[3].Cells[0].Controls.RemoveAt(0);
        accountOverview.Rows[3].Cells[0].Text = "-----";
    }

    public static DataTable createCustomTable()
    {
        DataTable dt = new DataTable();
        dt.TableName = "accountOverview";
        DataColumn Type = new DataColumn();
        Type.ColumnName = "Type";
        Type.DataType = typeof(System.String);
        Type.ReadOnly = true;
        DataColumn Info = new DataColumn();
        Info.ColumnName = "Info";
        Info.DataType = typeof(System.String);

        dt.Columns.Add(Type);
        dt.Columns.Add(Info);

        DataRow User = dt.NewRow();
        DataRow UserPassword = dt.NewRow();
        DataRow Age = dt.NewRow();
        DataRow Gender = dt.NewRow();
        DataRow FullName = dt.NewRow();
        DataRow Email = dt.NewRow();
        DataRow City = dt.NewRow();
        DataRow Address = dt.NewRow();
        DataRow ApartmentNumber = dt.NewRow();

        User["Info"] = DBService.GetResultFromSQL("SELECT Username FROM Accounts WHERE Username='" + Username + "'").ToString();
        User["Type"] = "שם משתמש:";
        UserPassword["Info"] = TextService.Decrypt(DBService.GetResultFromSQL("SELECT UserPassword FROM Accounts WHERE Username='" + Username + "'").ToString());
        UserPassword["Type"] = "סיסמא:";
        Age["Info"] = DBService.GetResultFromSQL("SELECT Age FROM Accounts WHERE Username='" + Username + "'").ToString();
        Age["Type"] = "גיל:";
        Gender["Info"] = DBService.GetResultFromSQL("SELECT Gender FROM Accounts WHERE Username='" + Username + "'").ToString();
        Gender["Type"] = "מין:";
        FullName["Info"] = DBService.GetResultFromSQL("SELECT FullName FROM Accounts WHERE Username='" + Username + "'").ToString();
        FullName["Type"] = "שם מלא:";
        Email["Info"] = DBService.GetResultFromSQL("SELECT Email FROM Accounts WHERE Username='" + Username + "'").ToString();
        Email["Type"] = "אימייל:";
        int CityID = int.Parse(DBService.GetResultFromSQL("SELECT City FROM Accounts WHERE Username='" + Username + "'").ToString());
        City["Info"] = DBService.GetResultFromSQL("SELECT CityName FROM Cities WHERE CityID=" + CityID).ToString();
        City["Type"] = "עיר מגורים:";
        Address["Info"] = DBService.GetResultFromSQL("SELECT Address FROM Accounts WHERE Username='" + Username + "'").ToString();
        Address["Type"] = "כתובת:";
        ApartmentNumber["Info"] = DBService.GetResultFromSQL("SELECT ApartmentNumber FROM Accounts WHERE Username='" + Username + "'").ToString();
        ApartmentNumber["Type"] = "מספר דירה:";

        dt.Rows.Add(User);
        dt.Rows.Add(UserPassword);
        dt.Rows.Add(Age);
        dt.Rows.Add(Gender);
        dt.Rows.Add(FullName);
        dt.Rows.Add(Email);
        dt.Rows.Add(City);
        dt.Rows.Add(Address);
        dt.Rows.Add(ApartmentNumber);
        return dt;
    }

    protected void accountOverview_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        accountOverview.EditIndex = -1;
        UpdateAccountOverview();
    }

    protected void accountOverview_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        switch (e.RowIndex)
        {
            case 0:
                Change("Username", e.RowIndex);
                break;
            case 1:
                Change("UserPassword", e.RowIndex);
                break;
            case 2:
                Change("Age", e.RowIndex);
                break;
            case 4:
                Change("FullName", e.RowIndex);
                break;
            case 5:
                Change("Email", e.RowIndex);
                break;
            case 6:
                Change("City", e.RowIndex);
                break;
            case 7:
                Change("Address", e.RowIndex);
                break;
            case 8:
                Change("ApartmentNumber", e.RowIndex);
                break;
        }

        accountOverview.EditIndex = -1;
        UpdateAccountOverview();
    }

    protected void accountOverview_RowEditing(object sender, GridViewEditEventArgs e)
    {
        error.Visible = false;
        accountOverview.EditIndex = e.NewEditIndex;
        string CityName = accountOverview.Rows[6].Cells[2].Text;
        TheCityName = CityName;
        UpdateAccountOverview();
        if (accountOverview.EditIndex == 6)
        {
            accountOverview.Rows[6].Cells[2].Controls[0].Visible = false;
            AddCitiesControl(CityName);
        }

        else if (accountOverview.EditIndex == 8 || accountOverview.EditIndex == 2)
        {
            TextBox theTextBox = ((TextBox)accountOverview.Rows[accountOverview.EditIndex].Cells[2].Controls[0]);
            theTextBox.AutoPostBack = true;
            theTextBox.MaxLength = 3;
            theTextBox.Width = Unit.Pixel(30);
            theTextBox.Attributes.Add("onkeypress", "return isNumberKey(event)");
            theTextBox.Attributes.Add("onpaste", "return clipboardContainsLetters(event)");
            theTextBox.Attributes.Add("autocomplete", "off");

        }
    }

    protected void AddCitiesControl(string CityName)
    {
        int CityID = int.Parse(DBService.GetResultFromSQL("SELECT CityID FROM Cities WHERE CityName='" + CityName + "'").ToString());
        DropDownList cities = new DropDownList();
        cities.ID = "CityName";
        cities.DataSourceID = "AccessDataSource1";
        cities.DataTextField = "CityName";
        cities.DataValueField = "CityName";
        cities.SelectedIndex = CityID - 1;
        accountOverview.Rows[6].Cells[2].Controls.Add(cities);
    }

    public void Change(string row, int rowIndex)
    {
        TextBox tb = ((TextBox)accountOverview.Rows[rowIndex].Cells[2].Controls[0]);
        string value = tb.Text;
        string SQLStr = "";

        switch (row)
        {
            case "Username":
                {
                    if (!AccountsService.UsernameExists(value) || value == Session["Username"].ToString())
                    {
                        Session["Username"] = value;
                        Username = Session["Username"].ToString();
                    }
                    else
                    {
                        error.Visible = true;
                        return;
                    }
                }

                break;

            case "UserPassword":
                {
                    if (value.Length < 6)
                    {
                        error.Text = ".סיסמא אינה חוקית. הסיסמא חייבת להכיל 6 תווים לפחות";
                        error.Visible = true;
                        return;
                    }
                    else
                        value = TextService.Encrypt(value);
                }
                break;
            case "Age":
                {
                    int result;
                    if (!int.TryParse(value, out result))
                    {
                        error.Text = ".גיל אינו חוקי";
                        error.Visible = true;
                        return;
                    }
                }
                break;
            case "FullName":
                if (!value.ToString().Contains(' '))
                {
                    error.Text = ".שם אינו חוקי";
                    error.Visible = true;
                    return;
                }
                break;

            case "Email":
                {
                    if (!Regex.IsMatch(value,
                @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                RegexOptions.IgnoreCase))
                    {
                        error.Text = ".האימייל אינו חוקי";
                        error.Visible = true;
                        return;
                    }

                    else if (AccountsService.EmailExists(value) && value != DBService.GetResultFromSQL("SELECT Email FROM Accounts WHERE Username='" + Session["Username"] + "'").ToString())
                    {
                        error.Text = ".האימייל שבחרת כבר קיים במערכת. אנא בחר אימייל אחר";
                        error.Visible = true;
                        return;
                    }
                }
                break;

            case "City":
                {
                    int CityID = ((DropDownList)accountOverview.Rows[6].Cells[2].Controls[1]).SelectedIndex + 1;
                    value = CityID.ToString();
                }
                break;
        }

        if (rowIndex != 2 && rowIndex != 6 && rowIndex != 8)
            SQLStr = "UPDATE Accounts SET " + row + "='" + value + "' WHERE Username='" + Username + "'";
        else
            SQLStr = "UPDATE Accounts SET " + row + "=" + value + " WHERE Username='" + Username + "'";
        DBService.ExeSQL(SQLStr);
    }
}