using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Admins_Orders : System.Web.UI.Page
{
    private static int searchedID; //משתנה שתפקידו לשמור מספר סידורי של הזמנה
    private static int totalPrice; //משתנה שתפקידו לשמור את סך העלות של הזמנה
    private static DataTable products; //משתנה שתפקידו לשמור את כל המוצרים שבהזמנה מסוימת

    protected void Page_Load(object sender, EventArgs e)
    {
        status.Text = null;
        totalPrice = 0; //עדכון המכיר הכולל של הזמנה ל0
    }
    protected void src_Click(object sender, EventArgs e) //פעולה המתחרשת כאשר המשתמש לוחץ על כפתור החיפוש
    {
        try
        {
            if (srcBy.SelectedIndex == 0) //מתרחש אם המשתמש רוצה לחפש הזמנה לפי שם משתמש
            {
                int UserID = int.Parse(DBService.GetResultFromSQL("SELECT ID FROM Accounts WHERE Username='" + orderSrc.Text + "'").ToString()); //מציאת המזהה של שם המשתמש שחופש
                DataTable Orders = DBService.SelectFromDB("SELECT OrderID FROM Orders WHERE UserID=" + UserID); //מציאת ההזמנות של המשתמש שחופש
                int i = 0; //משתנה עזר 
                OrderDetails.DataSource = DBService.SelectFromDB("SELECT OrderID FROM Orders WHERE UserID=" + UserID); //עדכון הגריד ויו
                OrderDetails.DataBind();
                foreach (DataRow row in Orders.Rows) //לולאה שעוברת על כל שורה בטבלת ההזמנות של המשתמש שחופש ומעדכנת כל שורה בגריד ויו
                {
                    searchedID = int.Parse(row.ItemArray[0].ToString()); //עדכון מזהה ההזמנה
                    FindOrder(i); //מציאת ההזמנה ועדכון הגריד ויו בהתאם
                    i++;
                }
            }
            else //מתרחש אם המשתמש רוצה לחפש הזמנה לפי מזהה הזמנה
            {
                searchedID = int.Parse(orderSrc.Text); //שמירת מזהה ההזמנה שהוזן במשתנה
                OrderDetails.DataSource = DBService.SelectFromDB("SELECT UserID FROM Orders WHERE OrderID=" + searchedID.ToString()); //עדכון הגריד ויו
                OrderDetails.DataBind();
                FindOrder(0); //מציאת ההזמנה הבודדת לה מזהה ההזמנה שחופש
            }
            GridView UDetails = (GridView)OrderDetails.Rows[0].Cells[0].FindControl("userDetails"); //מציאת הגריד ויו ה"פנימי" של פרטי המשתמש
            int userid = int.Parse(DBService.GetResultFromSQL("SELECT UserID FROM Orders WHERE OrderID=" + searchedID.ToString()).ToString()); //מציאת מזהה המשתמש לפי מזהה ההזמנה
            UDetails.DataSource = DBService.SelectFromDB("SELECT * FROM Accounts WHERE ID=" + userid.ToString()); //מציאת פרטי המשתמש שההזמנה שחופשה שייכת לו או ששם המשתמש שחופש שייך לו ועדכון הגריד ויו של פרטי המשתמש בשורה הראשונה של הגריד ויו המרכזי בהתאם
            UDetails.DataBind();
        }

        catch (Exception ex) //רץ עם לא נמצאה הזמנה עם המזהה שחופש או הזמנה ששייכת למשתמש שחופש
        {
            status.Text = "לא נמצאה ההזמנה המבוקשת";
        }
    }

    protected void FindOrder(int i) //פעולה שמוצאת הזמנה ומעדכנת שורה בגריד ויו המרכזי בהתאם
    {
        GridView ODetails = (GridView)OrderDetails.Rows[i].Cells[1].FindControl("orderDetails"); //מציאת הגריד ויו הפנימי שבשורה המבוקשת שמכיל פרטי הזמנה
        products = DBService.SelectFromDB("SELECT ProductID FROM OrdersDetails WHERE OrderID=" + searchedID.ToString()); //מציאת המזהה של המוצר בהזמנה
        ODetails.DataSource = DBService.SelectFromDB("SELECT Amount FROM OrdersDetails WHERE OrderID=" + searchedID.ToString()); //מציאת הכמות אותה מהמוצר שהוזמנה ועדכון הגריד ויו הפנימי שמכיל את פרטי ההזמנה בהתאם
        ODetails.DataBind();
    }

    protected void OrderDetailsRowDataBound(object sender, GridViewRowEventArgs e) //פעולה שרצה כאשר נוצרת שורה בגריד ויו פנימי שמכיל את פרטי ההזמנה בשורה כלשהיא בגריד ויו המרכזי
    {
        if (e.Row.RowType == DataControlRowType.DataRow) //רץ אם השורה מסוג שורה רגילה
        {
            int amount = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Amount")); //מציאת כמות המוצר שהוזמנה
            int productID = int.Parse(((DataRow)products.Rows[e.Row.RowIndex]).ItemArray[0].ToString()); //מציאת מזהה המוצר שהוזמן
            e.Row.Cells[0].Text = DBService.GetResultFromSQL("SELECT ProductName FROM Products WHERE ProductID=" + productID.ToString()).ToString(); //עדכון הגריד ויו הפנימי בהתאם
            e.Row.Cells[2].Text = (int.Parse(DBService.GetResultFromSQL("SELECT Price FROM Products WHERE ProductID=" + productID.ToString()).ToString()) * amount).ToString(); //מציאת המחיר שעלה המוצר בהתאם לכמות
            totalPrice += int.Parse(DBService.GetResultFromSQL("SELECT Price FROM Products WHERE ProductID=" + productID.ToString()).ToString()) * amount; //הוספת המחיר בהתאם לכמות למחיר הכללי של ההזמנה
        }

        else if (e.Row.RowType == DataControlRowType.Footer) //רץ אם השורה מסוג פוטר
        {
            e.Row.Cells[0].Text = "<b>סך הכל</b>: ₪" + totalPrice; //השמה של המכיר הכללי של ההזמנה בפוטר
            totalPrice = 0;
            e.Row.Cells[1].Text = "<b>תאריך הזמנה</b>: " + DBService.GetResultFromSQL("SELECT OrderDate FROM Orders WHERE OrderID=" + searchedID).ToString();
        }
    }

    protected void UserDetailsRowDataBound(object sender, GridViewRowEventArgs e) //פעולה שרצה כאשר נוצרת שורה בגריד ויו פנימי שמכיל את פרטי המשתמש בשורה כלשהיא בגריד ויו המרכזי
    {
        if (e.Row.RowType == DataControlRowType.DataRow) //רץ אם השורה מסוג שורה רגילה
        {
            string password = DataBinder.Eval(e.Row.DataItem, "UserPassword").ToString(); //קבלת הסיסמא ופיענוח שלה
            e.Row.Cells[2].Text = TextService.Decrypt(password);
        }
    }
}