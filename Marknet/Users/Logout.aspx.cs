using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Users_Logout : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e) //פעולה שרצה בעת עליית הדף
    {
        string username = "";
        if (Session["Username"] != null) //בדיקה האם ישנו משתמש מחובר - אם כן, הפעולה מוחקת את שם המשתמש הרשום בסשן
        {
            username = Session["Username"].ToString(); //שמירת שם המשתמש ששמור בסשן במשתנה לשימוש הפעולה
            Session["Username"] = null;

            if (Request.Cookies["rme_token"] != null) //בדיקה האם קיימת עוגייה שכותרתה מרקנט
            {
                HttpCookie cookie = new HttpCookie("rme_token"); //אם קיימת עוגייה שכותרתה מרקנט - קטע הקוד הבא מוחק אותה
                cookie.Expires = DateTime.Now.AddDays(-1); //השמת תאריך התפוגה של העוגייה ליום לפני היום הנוכחי כך שהעוגייה תמחק
                Response.Cookies.Add(cookie); //שמירת העוגייה
                DBService.ExeScalerSQL("UPDATE Accounts SET RememberMeToken='' WHERE Username='" + username+"'");
            }
        }
        Response.Redirect("Login.aspx"); //ניתוב המשתמש בחזרה לדף כניסת משתמש רשום
    }
}