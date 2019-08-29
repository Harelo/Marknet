using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Users_Confirm : System.Web.UI.Page
{
    //פעולה המתרחשת כאשר המשתמש נכנס לדף
    protected void Page_Load(object sender, EventArgs e)
    {
        //בדיקה האם ישנו Query בכתובת הדף בשם code
        if (Request.QueryString["code"] != null)
        {
            // השמת ערך הquery במשתנה
            string code = Request.QueryString["code"];
            //בדיקה האם ישנו משתמש לא מופעל שלו שייך הקוד בquery
            if (ConfirmAccount.Activate(code))
            {
                status.Text = "המשתמש הופעל! כעת תועבר לעמוד כניסת משתמש רשום.";
                Response.AddHeader("REFRESH", "4;URL=Login.aspx");
            }
                //ניתוב המשתמש לדף כניסת משתמש אם הקוד אינו משוייך לאף משתמש שאינו פעיל
            else
                Response.Redirect("Login.aspx");
        }
            //ניתוב המשתמש לדף כניסת משתמש רשום אם הכתובת לא חוקית
        else
            Response.Redirect("Login.aspx");
    }
}

