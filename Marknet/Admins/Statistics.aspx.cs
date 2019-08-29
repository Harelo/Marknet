using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Data;

public partial class Admins_Statistics : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        doStatistics();
    }

    protected void doStatistics() //מציאת סטטיסטיקות שונות
    {
        string SQLStr = "SELECT Count(Accounts.ID) FROM Accounts";
        stat1.Text = string.Format("<span style='font-weight: bold;'>מספר רשומים:</span> {0}", DBService.GetResultFromSQL(SQLStr).ToString());
        SQLStr = @"SELECT Count(Accounts.Gender) FROM Accounts WHERE Gender='זכר'";
        stat2.Text = string.Format("<span style='font-weight: bold;'>מספר בנים:</span> {0}", DBService.GetResultFromSQL(SQLStr).ToString());    
        SQLStr = @"SELECT Count(Accounts.Gender) FROM Accounts WHERE Gender='נקבה'";
        stat3.Text = string.Format("<span style='font-weight: bold;'>מספר בנות:</span> {0}", DBService.GetResultFromSQL(SQLStr).ToString());
        SQLStr = "SELECT Avg(Accounts.Age) FROM Accounts";
        double AvgAge = double.Parse(DBService.GetResultFromSQL(SQLStr).ToString());
        String.Format("{0:00.00}", AvgAge);
        stat4.Text = string.Format("<span style='font-weight: bold;'>ממוצע גילאים :</span> {0}", AvgAge);
        stat5.Text = string.Format("<span style='font-weight: bold;'>המוצר הנמכר ביותר :</span> {0}", DBService.ExeStoredProcedure("GetMostSoldName") + " - " + DBService.ExeStoredProcedure("GetMostSold"));
        stat6.Text = string.Format("<span style='font-weight: bold;'>המוצר הכי פחות נמכר :</span> {0}", DBService.ExeStoredProcedure("GetLeastSoldName") + " - " + DBService.ExeStoredProcedure("GetLeastSold"));
        SQLStr = "SELECT Sum(Amount) FROM OrdersDetails";
        stat7.Text = string.Format("<span style='font-weight: bold;'>מספר המוצרים שהוזמנו :</span> {0}", DBService.GetResultFromSQL(SQLStr));
        SQLStr = "SELECT Count(Products.ProductID) FROM Products";
        stat8.Text = string.Format("<span style='font-weight: bold;'>מספר מוצרים:</span> {0}", DBService.GetResultFromSQL(SQLStr).ToString());
        stat9.Text = string.Format("<span style='font-weight: bold;'>המשתמש שביצע הכי קצת הזמנות :</span> {0}", DBService.ExeStoredProcedure("GetNameWithLeastOrders") + " - " + DBService.ExeStoredProcedure("GetUserWithLeastOrders"));
        stat10.Text = string.Format("<span style='font-weight: bold;'>המשתמש שביצע הכי הרבה הזמנות :</span> {0}", DBService.ExeStoredProcedure("GetNameWithMostOrders") + " - " + DBService.ExeStoredProcedure("GetUserWithMostOrders"));
        SQLStr = "SELECT Count(OrderID) FROM Orders";
        stat11.Text = string.Format("<span style='font-weight: bold;'>מספר ההזמנות הכולל :</span> {0}", DBService.GetResultFromSQL(SQLStr));
        SQLStr = "SELECT Count(CommentID) FROM Comments";
        stat12.Text = string.Format("<span style='font-weight: bold;'>מספר התגובות הכולל :</span> {0}", DBService.GetResultFromSQL(SQLStr));
        SQLStr = "SELECT Count(CommentID) FROM Comments WHERE CommentType=1";
        stat13.Text = string.Format("<span style='font-weight: bold;'>מספר התגובות מהסוג שנבחר :</span> {0}", DBService.GetResultFromSQL(SQLStr));
    }

    protected void CommentTypes_SelectedIndexChanged(object sender, EventArgs e) //אירוע שמתחרש כאשר המשתמש משנה את הקטגוריה של התגובות שאת כמות התגובות מקטגרויה זו ברצונו לראות
    {
        int index = CommentTypes.SelectedIndex + 1;
        string SQLStr = "SELECT Count(CommentID) FROM Comments WHERE CommentType=" + index;
        stat13.Text = string.Format("<span style='font-weight: bold;'>מספר התגובות מהסוג שנבחר :</span> {0}", DBService.GetResultFromSQL(SQLStr));
    }
}