using System;
using System.Web.UI.WebControls;
using System.Web;
public partial class Users_Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e) //פעולה שרצה בעת עליית הדף
    {
        if (Session["Username"] != null) //פעולה שבודקת האם יש כבר משתמש מחובר, אם כן, הפעולה מנתבת את המשתמש לדף הבית
            Response.Redirect("Home.aspx");
    }

    protected void Auth(object sender, AuthenticateEventArgs e) //פעולה שרצה בעת אימות הנתונים בדף
    {
        if (Login.MemberLogin(memberLogin.UserName, memberLogin.Password)) //בדיקה האם קיים משתמש במסד הנתונים לו שם משתמש וסיסמא התואמים לשם המשתמש והסיסמא שהוכנסו
        { //אם כן, השורות הבאות רצות
            e.Authenticated = true; //סימון הדף כמאומת
            Session["Username"] = memberLogin.UserName; //שמירת שם המשתמש בסשן
            Session["Role"] = AccountsService.GetRole(Session["Username"].ToString()); //שמירת תפקיד המשתמש (משתמש רגיל או מנהל) בסשן
            if (memberLogin.RememberMeSet == true) //בדיקה האם המשתמש סימן את הכפתור "זכור אותי"
            {
                HttpCookie RememberMeCookie = new HttpCookie("rme_token"); //יצירת עוגייה חדשה שתשמור את טוקן ה"זכור אותי"
                string token = GenerateRememberMeToken(); //יצירת טוקן חדש והשמתו במשתנה
                RememberMeCookie.Value = token; //השמת הטוקן בערך העוגייה שיצרנו
                RememberMeCookie.Expires = DateTime.Now.AddYears(99); //קטע הקוד עושה שתאריך התפוגה של העוגייה יהיה בעוד 99 שנים, כלומר, העוגייה תשמר לתמיד
                Login.AddRememberMeTokenToAccount(memberLogin.UserName, token); //פעולה המוסיפה את טוקן ה"זכור אותי" למשתמש
                Response.Cookies.Add(RememberMeCookie); //שמירת העוגייה
            }
            Response.Redirect("Home.aspx"); //ניתוב המשתמש לדף הבית
        }
        e.Authenticated = false; //אם לא - סימון הדף כלא מאומת
    }

    protected string GenerateRememberMeToken() //יצירת טוקן "זכור אותי" חדש
    {
        Random random = new Random();
        string token = "";
        string characters = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

        for (int i = 0; i < 20; i++)
        {
            token += (characters[random.Next(characters.Length)]);
        }
        if (Login.CheckForToken(token))
            return GenerateRememberMeToken();
        else
            return token;
    }
}