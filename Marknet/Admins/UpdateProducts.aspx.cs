using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using System.Drawing;
using System.IO;

public partial class Admins_UpdateProducts : System.Web.UI.Page
{
    //משתנים שמשמשים לבדוק באיזה עמוד נמצא המשתמש בגריד ויו והאם הגריד ויו מראה תוצאות חיפוש או לא
    private static int pageIndex = 0, showingSrcResults = 0;
    private static string srced; //משתנה ששומר את שם המוצר שהמשתמש חיפש
    //פעולה שנקראת כאשר העמוד נפתח
    protected void Page_Load(object sender, EventArgs e)
    {
        //בדיקה האם הפעולה נקראה לאחר פוסט באק או שלא - אם כן, הפעולה מעדכנת את מספר העמוד בו המשתמש נמצא ל0 וגם את הגריד ויו
        if (!IsPostBack)
        {
            pageIndex = 0;
            productsTbl.DataSource = DBService.SelectFromDB("SELECT * FROM Products");
            productsTbl.DataBind();
        }
    }

    //פעולה שנקראת כאשר שורה בגריד ויו מעודכנת
    protected void productsTbl_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        //מציאת כל פרטי המוצר הדרושים לעדכון המוצר במסד הנתונים
        int productID = int.Parse(productsTbl.Rows[e.RowIndex].Cells[1].Text);
        string name = ((TextBox)productsTbl.Rows[e.RowIndex].Cells[2].Controls[0]).Text;
        int price = int.Parse(((TextBox)productsTbl.Rows[e.RowIndex].Cells[3].Controls[0]).Text);
        int stock = int.Parse(((TextBox)productsTbl.Rows[e.RowIndex].Cells[4].Controls[0]).Text);
        int CatagoryID = (((DropDownList)productsTbl.Rows[e.RowIndex].Cells[6].Controls[1]).SelectedIndex) + 1;

        string ProductOriginalName = DBService.GetResultFromSQL("SELECT ProductName FROM Products WHERE ProductID=" + productID).ToString(); //שינוי שם תמונת המוצר לשם החדש של המוצר
        string path = Server.MapPath(@"~\Images\");
        FileInfo file = new FileInfo(path + ProductOriginalName + ".jpg");
        File.Move(file.FullName, file.FullName.ToString().Replace(ProductOriginalName, name));

        if (fileUpload.PostedFile.FileName != "") // מציאת שם הקובץ אליו תעודכן תמונת המוצר אותו המשתמש רוצה לעדכן
        {
            string fileName = Path.GetFileName(fileUpload.PostedFile.FileName);
            fileUpload.PostedFile.SaveAs(Server.MapPath(@"~\Images\" + name + ".jpg"));
        }

        DBService.ExeSQL(@"UPDATE Products SET Picture='~/Images/" + name + ".jpg' WHERE ProductID=" + productID); //שיוך התמונה למוצר לפי שם המוצר החדש

        //קריאה לפעולה המעדכנת את פרטי המוצר במסד הנתונים
        updateProductsTbl(name, price, stock, CatagoryID, productID);
        //יציאה ממצב עריכה
        productsTbl.EditIndex = -1;
        if (showingSrcResults == 0)
            productsTbl.DataSource = DBService.SelectFromDB("SELECT * FROM Products");
        else
        {
            srced = name;
            productsTbl.DataSource = ProductService.GetProductByName(srced);
        }
        productsTbl.DataBind();
        fileUpload.Visible = false;
    }

    //פעולה המעדכנת את טבלת המוצרים במסד הנתונים
    protected void updateProductsTbl(string name, int price, int stock, int CatagoryID, int productID)
    {
        string SQLstr;
        SQLstr = string.Format("UPDATE Products SET ProductName = '{0}', Price = {1}, Stock = {2}, CatagoryID = {3} WHERE ProductID = {4}", name, price, stock, CatagoryID, productID);
        DBService.ExeSQL(SQLstr);
    }

    //פעולה הנקראת כאשר שורה בגריד ויו נכנסנת למצב עריכה
    protected void productsTbl_RowEditing(object sender, GridViewEditEventArgs e)
    {
        fileUpload.Visible = true;
        productsTbl.EditIndex = e.NewEditIndex;
        productsTbl.PageIndex = pageIndex;
        int ProductID = int.Parse(productsTbl.Rows[productsTbl.EditIndex].Cells[1].Text);
        int CatagoryID = int.Parse(DBService.GetResultFromSQL("SELECT CatagoryID FROM Products WHERE ProductID=" + ProductID.ToString()).ToString());
        if (showingSrcResults == 0)
            productsTbl.DataSource = DBService.SelectFromDB("SELECT * FROM Products");
        else
            productsTbl.DataSource = ProductService.GetProductByName(srced);
        productsTbl.DataBind();
        ((DropDownList)productsTbl.Rows[productsTbl.EditIndex].Cells[6].Controls[1]).SelectedIndex = CatagoryID - 1;
    }

    //פעולה הנקראת כאשר המשתמש מבטל את העריכה שערך
    protected void productsTbl_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        fileUpload.Visible = false;
        productsTbl.EditIndex = -1;
        if (showingSrcResults == 0)
            productsTbl.DataSource = DBService.SelectFromDB("SELECT * FROM Products");
        else
            productsTbl.DataSource = ProductService.GetProductByName(srced);
        productsTbl.DataBind();
    }

    //פעולה המרחשת כאשר המשתמש מחפש מוצר ותפקידה למצוא את המוצר במסד הנתונים ולהציגה בגריד ויו
    protected void src_Click(object sender, EventArgs e)
    {
        showingSrcResults = 1;
        srced = productSrc.Text;
        pageIndex = 0;
        productsTbl.DataSource = ProductService.GetProductByName(srced);
        productsTbl.DataBind();
    }

    //פעולה המרתשחת כאשר המשתמש מנסה להעביר עמוד בגריד ויו
    protected void productsTbl_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        productsTbl.PageIndex = e.NewPageIndex;
        pageIndex = e.NewPageIndex;
        if (showingSrcResults == 0)
            productsTbl.DataSource = DBService.SelectFromDB("SELECT * FROM Products");
        else
            productsTbl.DataSource = ProductService.GetProductByName(srced);
        productsTbl.DataBind();
    }

    //פעולה המרתחשת כאשר המשתמש רוצה לראות את כל המוצרים ותפקידה לעדכן את הגריד ויו בהתאם
    protected void showAll_Click(object sender, EventArgs e)
    {
        showingSrcResults = 0;
        productSrc.Text = "";
        srced = "";
        productsTbl.DataSource = DBService.SelectFromDB("SELECT * FROM Products");
        productsTbl.DataBind();
    }

    protected void productsTbl_RowDeleting(object sender, GridViewDeleteEventArgs e) //פעולה שמוחקת מוצר מהרשימה
    {
        string SQLstr = "DELETE FROM Products WHERE ProductID=" + int.Parse(productsTbl.Rows[e.RowIndex].Cells[1].Text) + ""; //מחיקת הרשומה
        DBService.ExeScalerSQL(SQLstr);
        productsTbl.DataSource = DBService.SelectFromDB("SELECT * FROM Products"); //עדכון מחדש של הגריד ויו
        productsTbl.DataBind();
    }

    protected void Add_Click(object sender, EventArgs e) //פעולה שרצה כאשר המשתמש לוחץ על הכפתור "הוסף מוצר חדש"
    {
        if (Add.Text == "הוסף מוצר חדש") //רץ כאשר המשתמש עדיין אינו בתפריט של הוספת מוצר חדש
        {
            NewCatagory.Visible = true; //פתיחת "תפריט" הוספת מוצר חדש
            NewCatagory.SelectedIndex = -1;
            NewName.Visible = true;
            NewName.Text = "שם מוצר";
            NewStock.Visible = true;
            NewStock.Text = "כמות מהמוצר";
            fileUpload.Visible = true;
            NewPrice.Visible = true;
            NewPrice.Text = "מחיר מוצר";
            Add.Text = "אשר";
            Cancel.Visible = true;
        }

        else if (Add.Text == "אשר") //רץ כאשר המשתמש מאשר את הוספת המוצר החדש
        {
            NewCatagory.Visible = false; //סגירת "תפריט" הוספת מוצר חדש
            NewName.Visible = false;
            NewStock.Visible = false;
            NewPrice.Visible = false;
            fileUpload.Visible = false;
            Add.Text = "הוסף מוצר חדש";
            Cancel.Visible = false;

            int CatagoryID = NewCatagory.SelectedIndex + 1; //קבלת נתונים לפי הנתונים שהוזנו ע"י המשתמש ויצירת מוצר חדש בהתאם
            string ProductName = NewName.Text;
            int Stock = int.Parse(NewStock.Text);
            int Price = int.Parse(NewPrice.Text);
            InsertNewProduct(CatagoryID, ProductName, Stock, Price);
            if (showingSrcResults == 0)
                productsTbl.DataSource = DBService.SelectFromDB("SELECT * FROM Products");
            else
                productsTbl.DataSource = ProductService.GetProductByName(srced);
            productsTbl.DataBind();
        }
    }

    protected void InsertNewProduct(int CatagoryID, string ProductName, int Stock, int Price) //פעולה שמקבלת נתונים על מוצר ומוסיפה את המוצר לטבלת המוצרים
    {
        int ProductID = ProductService.GetHighestProductID() + 1;
        string Picture = @"~\Images\" + ProductName + ".jpg";
        string SQLStr = "INSERT INTO Products (ProductID, ProductName, Price, Stock, Picture, CatagoryID) VALUES (" + ProductID + ", '" + ProductName + "', " + Price + ", " + Stock + ", '" + Picture + "', " + CatagoryID + ")";
        string fileName = Path.GetFileName(fileUpload.PostedFile.FileName);
        fileUpload.PostedFile.SaveAs(Server.MapPath(@"~\Images\" + ProductName + ".jpg"));
        DBService.ExeSQL(SQLStr);
    }

    protected void Cancel_Click(object sender, EventArgs e) //פעולה שרצה כאשר המשתמש רוצה לצאת מ"תפריט" הוספת מוצר חדש מבלי להוסיף את המוצר
    {
        NewCatagory.Visible = false;
        NewName.Visible = false;
        NewName.Text = "שם מוצר";
        NewStock.Visible = false;
        NewStock.Text = "כמות מהמוצר";
        NewPrice.Visible = false;
        NewPrice.Text = "מחיר מוצר";
        NewCatagory.SelectedIndex = 0;
        fileUpload.Visible = false;
        Add.Text = "הוסף מוצר חדש";
        Cancel.Visible = false;
    }
}