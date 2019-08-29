using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admins_ReadComments : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Comments.DataSource = DBService.SelectFromDB("SELECT * FROM Comments");
        Comments.DataBind();
    }

    protected void Comments_RowDataBound(object sender, GridViewRowEventArgs e) //פעולה שרצה כאשר שורה נוצרת בגריד ויו
    {
        if (e.Row.RowType == DataControlRowType.DataRow) //רץ אם השורה מסוג שורה רגילה
        {
            int UserID = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "UserID")); //המרת המזהה של שם המשתמש לשם המשתמש עצמו
            e.Row.Cells[1].Text = DBService.GetResultFromSQL("SELECT Username FROM Accounts WHERE ID=" + UserID).ToString();
            int CommentTypeID = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "CommentType")); //המרת מזהה סוג התגובה לסוג התגובה עצמו
            e.Row.Cells[3].Text = DBService.GetResultFromSQL("SELECT CatagoryName FROM CommentCatagories WHERE CatagoryID=" + CommentTypeID).ToString();    
        }
    }


    protected void Comments_PageIndexChanging(object sender, GridViewPageEventArgs e) //פעולה שרצה כאשר המשתמש מחליף עמוד בגריד ויו
    {
        Comments.PageIndex = e.NewPageIndex;
        Comments.DataBind();
    }
}