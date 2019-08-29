using System.Data;

public class ProductService
{
    //פעולה המחזירה את כל המוצרים
    public static DataTable GetAllProducts()
    {
        string SQLstr = @"SELECT ProductID, ProductName, Stock, Price, Picture FROM Products;";
        DataTable dt = DBService.SelectFromDB(SQLstr);
        return dt;
    }

    //פעולה המחזירה את כל המוצרים ששייכים לקטגוריה מסוימת
    public static DataTable GetProductByCategory(int catID)
    {
        DataTable dt = DBService.SelectFromDB(@"SELECT ProductID, ProductName, Stock, Price, Picture FROM Products WHERE CatagoryID=" + catID + ";");
        return dt;
    }

    //פעולה שמחזירה טבלה עם פרטי מוצר לפי שם המוצר
    public static DataTable GetProductByName(string name)
    {
        DataTable dt = DBService.SelectFromDB(@"SELECT * FROM Products WHERE ProductName LIKE '%" + name + "%'");
        return dt;
    }

    //פעולה אשר מעדכנת את הכמות ממוצר מסוים לפי מספר הזיהוי שלו
    public static void UpdateProductStock(int ProductID, int newAmount)
    {
        string SQLStr = "UPDATE Products SET Stock=" + newAmount.ToString() + " WHERE ProductID=" + ProductID.ToString();
        DBService.ExeSQL(SQLStr);
    }

    //פעולה המחזירה את הכמות ממוצר מסוים לפי המספר המזהה שלו
    public static int GetStock(int ProductID)
    {
        string SQLstr = "SELECT Stock FROM Products WHERE ProductID=" + ProductID.ToString();
        int stock = int.Parse(DBService.GetResultFromSQL(SQLstr).ToString());
        return stock;
    }

    public static int GetHighestProductID() //פעולה שמחזירה את המזהה של המוצר בעל המזהה הגבוהה ביותר
    {
        string SQLstr = "SELECT Max(Products.ProductID) FROM Products";
        object max = DBService.GetResultFromSQL(SQLstr);
        if (max == null)
            return 1;
        else
            return int.Parse(max.ToString());
    }
}