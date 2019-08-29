using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admins_AdminsMaster : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Username"] != null)
        {
            if (Session["Role"] != null && Session["Role"].ToString() == "admin")
            {
                loggedUser.Text = Session["Username"] + " ,ברוך הבא";
            }
            else
                Response.Redirect("~/Users/Home.aspx");
        }
        else
            Response.Redirect("~/Users/Login.aspx");
    }
}
