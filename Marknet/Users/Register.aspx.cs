using System;
using System.Drawing;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Text;
using System.IO;
using System.Security.Cryptography;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e) //פעולה שרצה בעת עליית הדף
    {
        if (Session["Username"] != null) //בדיקה האם ישנו משתמש רשום שמחובר ואם כן ניתוב המשתמש בחזרה לדף הבית
            Response.Redirect("Home.aspx");
        disableValidators(); //העברת המאמתים למצב לא פעיל
    }

    protected void register_Click(object sender, EventArgs e) //פעולה שרצה בעת הלחיצה על הכפתור "הרשם" שבודקת האם כל הפרטים שהוכנסו תקינים ואם כן רושמת את המשתמש החדש
    {
        enableValidators();
        string response;
        Page.Validate("registerGroup");
        status.ForeColor = Color.Red;

        if (!confirmPassValidator.IsValid)
        {
            status.Text = confirmPassValidator.ErrorMessage;
            disableValidators();
            return;
        }

        if (!invalidEmail.IsValid)
        {
            status.Text = invalidEmail.ErrorMessage;
            disableValidators();
            return;
        }

        if (!ageValidator.IsValid)
        {
            status.Text = ageValidator.ErrorMessage;
            disableValidators();
            return;
        }

        if (!invalidUsername.IsValid)
        {
            status.Text = invalidUsername.ErrorMessage;
            disableValidators();
            return;
        }

        if (!emailValidator.IsValid)
        {
            status.Text = emailValidator.ErrorMessage;
            disableValidators();
            return;
        }

        if (!nameValidator.IsValid)
        {
            status.Text = nameValidator.ErrorMessage;
            disableValidators();
            return;
        }

        try
        {
            string gender = "";
            switch (Gender.SelectedIndex)
            {
                case 0:
                    gender = "זכר";
                    break;
                case 1:
                    gender = "נקבה";
                    break;
            }
            response = Register.MemberRegister(user.Text, pass.Text, int.Parse(age.Text), gender, fullName.Text, email.Text, confirmPass.Text, City.SelectedIndex+1, address.Text, int.Parse(apartmentNum.Text));
        }

        catch (Exception ex)
        {
            response = "error";
        }

        if (response == "success")
        {
            status.Text = "נרשמת בהצלחה! אימייל נשלח אליך עם הוראות כיצד להשלים את תהליך הרישום. כעת תועבר לעמוד הבית.";
            status.ForeColor = Color.LightGreen;
            SendRegistrationEmail(email.Text, user.Text);
            user.Text = "";
            pass.Text = "";
            age.Text = "";
            fullName.Text = "";
            email.Text = "";
            address.Text = "";
            Gender.SelectedIndex = -1;
            Response.AddHeader("REFRESH", "4;URL=Home.aspx");
        }
    }

    protected void disableValidators() //פעולה שמעבירה את כל המאמתים בדף למצב לא פעיל
    {
        confirmPassValidator.Enabled = false;
        invalidUsername.Enabled = false;
        invalidEmail.Enabled = false;
        ageValidator.Enabled = false;
        emailValidator.Enabled = false;
        nameValidator.Enabled = false;
    }

    protected void enableValidators() //פעולה שמעבירה את כל המאמתים בדף למצב פעיל
    {
        confirmPassValidator.Enabled = true;
        invalidUsername.Enabled = true;
        invalidEmail.Enabled = true;
        ageValidator.Enabled = true;
        emailValidator.Enabled = true;
        nameValidator.Enabled = true;
    }

    protected void invalidEmail_ServerValidate(object source, System.Web.UI.WebControls.ServerValidateEventArgs args) //פעולה שרצה כאשר מאמת המייל מאמת את המייל - זהו הקוד לפיו המאמת יודע אם האימות בוצע בהצלחה או לא
    {
        if (AccountsService.EmailExists(email.Text))
            args.IsValid = false;
        else if (!AccountsService.EmailExists(email.Text))
            args.IsValid = true;
    }

    protected void invalidUsername_ServerValidate(object source, System.Web.UI.WebControls.ServerValidateEventArgs args) //פעולה שרצה כאשר מאמת שם המשתמש מאמת את שם המשתמש - זהו הקוד לפיו המאמת יודע אם האימות בוצע בהצלחה או לא
    {
        if (AccountsService.UsernameExists(user.Text))
            args.IsValid = false;
        else if (!AccountsService.UsernameExists(email.Text))
            args.IsValid = true;
    }

    protected void SendRegistrationEmail(string address, string username) //שליחת הודעה למשתמש שנרשם עם פרטי בקשת לאישור המייל
    {
        string code = GenerateCode();
        int port = HttpContext.Current.Request.Url.Port;
        string link = "http://localhost:" + port + "/Marknet/Users/Confirm.aspx?code=" + code;
        string message = @"
<html lang=""EN"">
<body style =""text-align:left; direction:rtl"">
<center>
<font color=""green"" size=""6"">
נרשמת לMarknet בהצלחה!
</font>
<br />
<font color=""black"" size=""3"">
שלום " + username + @", אנחנו שמחים להודיע לך שנרשמת לאתרנו בהצלחה! <br /> יש רק עוד שלב אחד, אנא לחץ <a href=""" + link + @""">כאן</a> כדי להפעיל את המשתמש שלך!
</center>
</body>
</html>";
        EmailService.SendEmail(address, "Marknet - אישור רישום משתמש", message); //שליחת המייל
        ConfirmAccount.AddCodeToAccount(username, code); //קישור בין קוד האימות למשתמש
    }

    protected string GenerateCode() //יצירת קוד אימות חדש
    {
        Random random = new Random();
        string code = "";
        string characters = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

        for (int i = 0; i < 20; i++)
        {
            code += (characters[random.Next(characters.Length)]);
        }
        if (ConfirmAccount.CheckForCode(code))
            return GenerateCode();
        else
            return code;
    }
}