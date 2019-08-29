using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

public partial class Users_Forgot : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e) //פעולה הרצה כאשר המשתמש נכנס לדף ותפקידה לבדוק האם המשתמש מחובר ואם כן לנתב אותו לדף הבית
    {
        if (Session["Username"] != null)
            Response.Redirect("Home.aspx");
    }

    protected void submit_Click(object sender, EventArgs e) // פעולה שרצה כאשר המשתמש לוחץ על הכפתור "שלח" שתפקידה לשלוח למשתמש מייל עם הפרטים המזהים שלו
    {
        string response = ForgotUser.ForgotUserOrEmail(userOrEmail.Text); //קריאה לפעולה שתפקידה לבדוק האם ישנו אימייל שזהה לאימייל שהמשתמש הזין או האם קיים אימייל המשוייך לשם המשתמש שהמשתמש הזין
        if (response != "notFound") //רץ אם נמצא אימייל שמשוייך לשם המשתמש שהמשתמש הזין או מייל שזהה למייל שהמשתמש הזין
        {
            SendForgotEmail(response, ForgotUser.getDetailsByEmail(response)[0], ForgotUser.getDetailsByEmail(response)[1]); // שליחת מייל עם פרטי המשתמש למייל של המשתמש
            status.Text = "!הודעה עם פרטי המשתמש שלך נשלחה למייל שלך"; //הצגת הודעה מתאימה
            status.ForeColor = Color.LightGreen;
        }
        else
        {
            status.Text = "!שם משתמש/אימייל לא נמצא"; //הצגת הודעת שגיאה 
            status.ForeColor = Color.Red;
        }
    }

    protected void SendForgotEmail(string address, string username, string password)
    {
        string message = @"
<html lang=""EN"">
<body style =""text-align:left; direction:rtl"">
<center>
<font color=""green"" size=""6"">
שכחת את הסיסמא שלך? אל דאגה!
</font>
<br />
<font color=""black"" size=""3"">
פרטי המשתמש שלך הם: <br/><b>שם משתמש</b>: " + username + "<br/><b>סיסמא</b>: " + password + @"
</center>
</body>
</html>";
        string subject = "Marknet - פרטי משתמש";
        EmailService.SendEmail(address, subject, message);
    }
}