using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Users_GeneralMaster : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Username"] != null)
        {
            login.Text = "יציאה מהמשתמש";
            loggedUser.Text = Session["Username"] + " ,ברוך הבא";
            HyperLink5.Visible = true;
            if (Session["Role"].ToString() == "admin")
                HyperLink6.Visible = true;
        }
    }

    protected void home_Click(object sender, EventArgs e)
    {
        Response.Redirect("Home.aspx");
    }

    protected void shop_Click(object sender, EventArgs e)
    {
        Response.Redirect("Shop.aspx");
    }

    protected void basket_Click(object sender, EventArgs e)
    {
        Response.Redirect("Basket.aspx");
    }

    protected void login_Click(object sender, EventArgs e)
    {
        if (login.Text == "כניסת משתמש")
            Response.Redirect("Login.aspx");
        else
            Response.Redirect("Logout.aspx");
    }

    protected void comments_Click(object sender, EventArgs e)
    {
        Response.Redirect("Comments.aspx");
    }
}
