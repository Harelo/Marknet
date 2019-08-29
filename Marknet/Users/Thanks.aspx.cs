using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Users_Thanks : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e) //פעולה שרצה בעת עליית הדף
    {
        if (Session["PurchaseSuccessful"] != null && (bool)Session["PurchaseSuccessful"] == true) //בדיקה האם הקנייה אושרה בהתאם למידע בסשן
        {
            Response.AddHeader("REFRESH", "4;URL=Home.aspx"); //אם הקנייה אכן אושרה - ניתוב המשתמש לדף הבית
            Session["PurchaseSuccessful"] = null; //מחיקת אישור הקנייה השמור בסשן
        }
        else
            Response.Redirect("Home.aspx"); //ניתוב המשתמש חזרה לדף הבית אם אין מידע המעיד על כך שקנייה שהתרחשה אושרה בסשן
    }
}