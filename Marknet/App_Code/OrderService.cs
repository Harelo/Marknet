using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Drawing;


public class OrderService
{
    public static void InsertIntoOrder(int UserID, string OrderDate) //פעולה שמכניסה הזמנה לטבלת ההזמנות במסד הנתונים
    {
        string SQLstr = "INSERT INTO Orders (UserID, OrderDate) VALUES (" + UserID + ", '" + OrderDate + "')"; //פקודת הSQL שתופעל
        DBService.ExeSQL(SQLstr); //הפעלת הפקודה
    }

    public static void InsertIntoOrderDetails(int OrderID, int ProductID, int Amount) //פעולה שמכניסה פרטי הזמנה לטבלת פרטי ההזמנות במסד הנתונים
    {
        if (Amount <= ProductService.GetStock(ProductID)) //פעולה הבודקת האם כמות המוצר המבוקשת אינה גדולה מכמות המוצר בחנות
        {
            string SQLstr = string.Format(@"INSERT INTO OrdersDetails (OrderID, ProductID, Amount) VALUES ({0}, {1}, {2})", OrderID, ProductID, Amount); //פקודת הSQL שתופעל
            DBService.ExeSQL(SQLstr); //הפעלת הפקודה
        }

        else
            throw new Exception("Order.InvalidAmount"); //זריקת שגיאה אם אין מספיק מוצרים במלאי
    }

    public static int GetNextOrderID() // פעולה שמחזירה את המזהה של ההזמנה האחרונה או אחד אם הטבלה ריקה
    {
        string SQLstr = "SELECT Max(Orders.OrderID) FROM Orders";
        object max = DBService.GetResultFromSQL(SQLstr);
        if (max == null)
            return 1;
        else
            return int.Parse(max.ToString());
    }

    public static DataTable GetDetails(int id) //פעולה שמחזירה פרטים של הזמנה לפי מזהה ההזמנה
    {
        string SQLStr = "SELECT ProductID, Amount FROM OrdersDetails WHERE OrderID=" + id;
        DataTable orders = DBService.SelectFromDB(SQLStr);
        return orders;
    }
}