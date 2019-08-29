using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admins_UpdateUsers : System.Web.UI.Page
{
    private static int count = 0;
    //משתנים שמשמשים לבדוק באיזה עמוד נמצא המשתמש בגריד ויו והאם הגריד ויו מראה תוצאות חיפוש או לא
    private static int pageIndex = 0, showingSrcResults = 0;
    private static string srced; //משתנה ששומר את שם המשתמש שהמשתמש חיפש

    protected void Page_Load(object sender, EventArgs e) //פעולה שרצה בעת הכניסה לדף
    {
        count = 0;
        //בדיקה האם הפעולה נקראה לאחר פוסט באק או שלא - אם כן, הפעולה מעדכנת את מספר העמוד בו המשתמש נמצא ל0 וגם את הגריד ויו
        if (!IsPostBack)
        {
            pageIndex = 0;
            usersTbl.DataSource = DBService.SelectFromDB("SELECT ID, Username, UserPassword, Age, Gender, FullName, Email, Role, City, Address, ApartmentNumber FROM Accounts"); //הצגת הנתונים מטבלת המשתמשים
            usersTbl.DataBind();
        }
    }

    protected void usersTbl_RowDeleting(object sender, GridViewDeleteEventArgs e) //פעולה שרצה בעת מחיקת רשומה
    {
        string SQLstr = "DELETE FROM Accounts WHERE ID=" + int.Parse(usersTbl.Rows[e.RowIndex].Cells[1].Text) + ""; //מחיקת הרשומה
        DBService.ExeScalerSQL(SQLstr);
        usersTbl.DataSource = DBService.SelectFromDB("SELECT ID, Username, UserPassword, Age, Gender, FullName, Email, Role, City, Address, ApartmentNumber FROM Accounts"); //עדכון מחדש של הגריד ויו
        usersTbl.DataBind();
        count--;
    }

    protected void usersTbl_RowEditing(object sender, GridViewEditEventArgs e) //פעולה שרצה בעת כניסה למצב עדכון שורה
    {
        usersTbl.EditIndex = e.NewEditIndex;
        usersTbl.PageIndex = pageIndex;
        if (showingSrcResults == 0)
            usersTbl.DataSource = DBService.SelectFromDB("SELECT ID, Username, UserPassword, Age, Gender, FullName, Email, Role, City, Address, ApartmentNumber FROM Accounts");
        else
            usersTbl.DataSource = AccountsService.GetDetailsByName(srced);
        string City = usersTbl.Rows[usersTbl.EditIndex].Cells[9].Text;
        usersTbl.DataBind();
        ((TextBox)usersTbl.Rows[e.NewEditIndex].Cells[3].Controls[0]).Text = TextService.Decrypt(((TextBox)usersTbl.Rows[e.NewEditIndex].Cells[3].Controls[0]).Text);
        int CityID = int.Parse(DBService.GetResultFromSQL("SELECT CityID FROM Cities WHERE CityName='" + City + "'").ToString());
        ((DropDownList)usersTbl.Rows[usersTbl.EditIndex].Cells[9].Controls[1]).SelectedIndex = CityID - 1;
    }

    protected void usersTbl_RowUpdating(object sender, GridViewUpdateEventArgs e) //עדכון פרטי המשתמש
    {
        int UserId = int.Parse(usersTbl.Rows[e.RowIndex].Cells[1].Text); //הבאת המידע מהטבלה על פי הנתונים שהמשתמש הזין
        string username = ((TextBox)usersTbl.Rows[e.RowIndex].Cells[2].Controls[0]).Text;
        string password = ((TextBox)usersTbl.Rows[e.RowIndex].Cells[3].Controls[0]).Text;
        int age = int.Parse(((TextBox)usersTbl.Rows[e.RowIndex].Cells[4].Controls[0]).Text);
        string gender = ((TextBox)usersTbl.Rows[e.RowIndex].Cells[5].Controls[0]).Text;
        string fullName = ((TextBox)usersTbl.Rows[e.RowIndex].Cells[6].Controls[0]).Text;
        string email = ((TextBox)usersTbl.Rows[e.RowIndex].Cells[7].Controls[0]).Text;
        string role = ((TextBox)usersTbl.Rows[e.RowIndex].Cells[8].Controls[0]).Text;
        string City = (((DropDownList)usersTbl.Rows[e.RowIndex].Cells[9].Controls[1]).SelectedValue);
        int CityID = int.Parse(DBService.GetResultFromSQL("SELECT CityID FROM Cities WHERE CityName='" + City + "'").ToString());
        string address = ((TextBox)usersTbl.Rows[e.RowIndex].Cells[10].Controls[0]).Text;
        int apartmentNumber = int.Parse(((TextBox)usersTbl.Rows[e.RowIndex].Cells[11].Controls[0]).Text);

        //עדכון פרטי המשתמש לפי הנתונים שהמשתמש הזין
        string SQLStr = @"UPDATE Accounts SET Username='" + username + "', UserPassword='" + TextService.Encrypt(password) + "', Age=" + age + ", Gender='" + gender + "', FullName='" + fullName + "', Email='" + email + "', Role='" + role + "', Address='" + address + "', City=" + CityID + ", ApartmentNumber=" + apartmentNumber + " WHERE ID=" + UserId;
        DBService.ExeSQL(SQLStr); //ביצוע העידכון
        usersTbl.EditIndex = -1; //יציאה ממצב עדכון
        if (showingSrcResults == 0) //עדכון הגריד ויו
            usersTbl.DataSource = DBService.SelectFromDB("SELECT ID, Username, UserPassword, Age, Gender, FullName, Email, Role, City, Address, ApartmentNumber FROM Accounts");
        else
            usersTbl.DataSource = AccountsService.GetDetailsByName(srced);
        usersTbl.DataBind();
    }

    protected void usersTbl_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e) //פעולה הנקראת כאשר המשתמש מבטל את העריכה שערך
    {
        usersTbl.EditIndex = -1;
        if (showingSrcResults == 0)
            usersTbl.DataSource = DBService.SelectFromDB("SELECT ID, Username, UserPassword, Age, Gender, FullName, Email, Role, City, Address, ApartmentNumber FROM Accounts"); //הצגת הנתונים מטבלת המשתמשים
        else
            usersTbl.DataSource = AccountsService.GetDetailsByName(srced);
        usersTbl.DataBind();
    }

    protected void src_Click(object sender, EventArgs e) //פעולה המרחשת כאשר המשתמש מחפש משתמש ותפקידה למצוא את המוצר במסד הנתונים ולהציגה בגריד ויו
    {
        showingSrcResults = 1;
        pageIndex = 0;
        srced = userSrc.Text;
        usersTbl.DataSource = AccountsService.GetDetailsByName(srced);
        usersTbl.DataBind();
    }

    protected void usersTbl_PageIndexChanging(object sender, GridViewPageEventArgs e) //פעולה המתרחשת כאשר המשתמש מחליף עמוד
    {
        usersTbl.PageIndex = e.NewPageIndex;
        pageIndex = e.NewPageIndex;
        if (showingSrcResults == 0)
            usersTbl.DataSource = DBService.SelectFromDB("SELECT ID, Username, UserPassword, Age, Gender, FullName, Email, Role, City, Address, ApartmentNumber FROM Accounts");
        else
            usersTbl.DataSource = AccountsService.GetDetailsByName(srced);
        usersTbl.DataBind();
    }

    protected void showAll_Click(object sender, EventArgs e) //הראה את כל המשתמשים
    {
        showingSrcResults = 0;
        srced = "";
        userSrc.Text = "";
        usersTbl.DataSource = DBService.SelectFromDB("SELECT ID, Username, UserPassword, Age, Gender, FullName, Email, Role, City, Address, ApartmentNumber FROM Accounts");
        usersTbl.DataBind();
    }

    protected void usersTbl_RowDataBound(object sender, GridViewRowEventArgs e) //פעולה שרצה בכל יצירת שורה בגריד ויו
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            count++;
            if (e.Row.RowIndex != usersTbl.EditIndex) //בדיקה האם השורה במצב עריכה
            {
                string password = DataBinder.Eval(e.Row.DataItem, "UserPassword").ToString(); //קבלת הסיסמא ופיענוח שלה
                e.Row.Cells[3].Text = TextService.Decrypt(password);
                int CityID = int.Parse(DataBinder.Eval(e.Row.DataItem, "City").ToString());
                string City = DBService.GetResultFromSQL("SELECT CityName FROM Cities WHERE CityID=" + CityID).ToString();
                e.Row.Cells[9].Text = City;
            }
        }
    }
    protected void usersTbl_DataBound(object sender, EventArgs e)
    {
        memberAmount.Text = "נמצאו " + count + " משתמשים";
    }
}