using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Drawing;
using System.Web.UI.WebControls;

public partial class Users_Comments : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e) //פעולה שרצה בעת עליית הדף
    {
        if (Session["Username"] == null) //בדיקה האם ישנו משתמש מחובר - אם לא האדם שנכנס לדף מועבר לדף כניסת משתמש רשום
            Response.Redirect("Login.aspx");
    }

    protected void Comment_Click(object sender, EventArgs e) //פעולה שרצה כאשר המשתמש לוחץ על הכפתור "הגב"
    {
        if (CommentText.Text == "" || CommentText.Text == null) //בדיקה האם תוכן התגובה לא ריק ואם הוא כן  - הצגת הודעת שגיאה בהתאם
            status.Text = ".אנא הזן תוכן לתגובה";
        else if (CommentText.Text.Contains("'")) //בדיקה האם תוכן התגובה מכיל את התו ' ואם כן הצגת שגיאה בהתאם
            status.Text = ".תו לא חוקי נמצא בתגובתך";
        else //רץ אם אין שום בעיה בתוכן התגובה
        {
            string text = CommentText.Text; //השמת תוכן התגובה במשתנה מסוג מחרוזת
            int UserID = int.Parse(DBService.GetResultFromSQL("SELECT ID FROM Accounts WHERE Username='" + Session["Username"].ToString() + "'").ToString()); //מציאת המזהה של המשתמש לפי שם המשתמש שלו
            DateTime date = DateTime.Now; //השמת התאריך הנוכחי במשתנה מסוג תאריך ושעה
            int type = CommentType.SelectedIndex; //השמת מזהה סוג התגובה לפי מה שבחר המשתנה במשתנה מסוג מספר שלם
            CommentService.AddComment(text, UserID, type + 1); //הוספת התגובה למסד הנתונים עם כל הפרטים הנחוצים
            status.ForeColor = Color.LightGreen; //הצגת הודעה שהתגובה נשלחה בהצלחה
            status.Text = "!התגובה נשלחה בהצלחה";
            CommentText.Text = null;
        }
    }
}