using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Users_Shop : System.Web.UI.Page
{
    public static int chosen = -1, showingSrcResults = 0; //משתנים שמכילים מידע לגבי האם הגריד ויו מראה תוצאות חיפוש ואיזו קטגוריה המשתמש בחר
    protected void Page_Load(object sender, EventArgs e) //פעולה שרצה בעת עליית הדף
    {
        if (!IsPostBack) //בדיקה האם הפעולה נקרה בעת הרצת הדף בפעם הראשונה ואם כן - השמת ערך 0 במשתנה שבודק האם הגריד ויו מראה תוצאות חיפוש
            showingSrcResults = 0;
        if (Session["Basket"] == null) //בדיקה האם ישנו סל קניות שמור בסשן ואם לא יצירת אחד
        {
            Session["Basket"] = new DataTable();
            ((DataTable)Session["Basket"]).Columns.Add("ProductID");
            ((DataTable)Session["Basket"]).Columns.Add("ProductName");
            ((DataTable)Session["Basket"]).Columns.Add("Stock");
            ((DataTable)Session["Basket"]).Columns.Add("Price");
            ((DataTable)Session["Basket"]).Columns.Add("AmountRequired");
        }

        if (showingSrcResults == 0) //בדיקה האם הגריד ויו לא מראה כעת תוצאות חיפוש ועדכון הגריד ויו בהתאם
        {
            products.DataSource = ProductService.GetAllProducts();
            products.DataBind();
            if (products.Rows.Count == 0)
                products.EmptyDataText = "<font color='red' size='4pt'>אין מוצרים בחנות!</font>";
            filters_SelectedIndexChanged(sender, e);
        }
    }


    protected void products_RowCommand(object sender, GridViewCommandEventArgs e) //פעולה שרצה כאשר פקודה נקראת מהגריד ויו
    {
        invalidAmount.Visible = false;
        if (e.CommandName == "addToBasket") //בדיקה האם הפקודה היא פקודת הוספה לסל , אם כן - קטע הקוד הבא מוסיף את המוצר לסל הקניות של המשתמש
        {
            int rowNum = int.Parse((e.CommandArgument).ToString());
            int ProductID = int.Parse(products.Rows[rowNum].Cells[0].Text);

            foreach (DataRow dr in ((DataTable)Session["Basket"]).Rows)
            {
                if (dr["ProductID"].ToString() == ProductID.ToString())
                {
                    if (int.Parse(dr["AmountRequired"].ToString()) + 1 <= int.Parse(dr["Stock"].ToString())) //בדיקה האם הכמות המבוקשת לא עולה על הכמות מהמוצר במלאי
                    {
                        dr["AmountRequired"] = (int.Parse(dr["AmountRequired"].ToString()) + 1).ToString(); //העלאת הכמות המבוקשת מהמוצר שהמשתמש רוצה לקנות ב1
                        return; //הפסקת הפעולה
                    }
                    invalidAmount.Visible = true;
                    return;
                }
            }

            if (ProductService.GetStock(ProductID) >= 1)
            {
                string ProductName = products.Rows[rowNum].Cells[1].Text;
                int Stock = int.Parse(products.Rows[rowNum].Cells[2].Text);
                int Price = int.Parse(products.Rows[rowNum].Cells[3].Text);
                addProductToBasket(ProductID, ProductName, Stock, Price); //הוספת המוצר לסל הקניות של המשתמש
            }
            else
                invalidAmount.Visible = true;
        }
    }

    protected void addProductToBasket(int id, string name, int stock, int price) //פעולה המוסיפה מוצר לסל הקניות של המשתמש
    {
        if (!((DataTable)Session["Basket"]).Columns.Contains(id.ToString()))
        {
            DataRow newPInBasket = ((DataTable)Session["Basket"]).NewRow();
            newPInBasket[0] = id;
            newPInBasket[1] = name;
            newPInBasket[2] = stock;
            newPInBasket[3] = price;
            newPInBasket[4] = 1;
            ((DataTable)Session["Basket"]).Rows.Add(newPInBasket);
        }
    }

    protected void products_PageIndexChanging(object sender, GridViewPageEventArgs e) //פעולה שרצה כאשר המשתמש מחליף עמוד בגריד ויו
    {
        products.PageIndex = e.NewPageIndex;
        products.DataBind();
    }

    protected void filters_SelectedIndexChanged(object sender, EventArgs e) //פעולה שרצה כאשר המשתמש בוחר קטגוריה חדשה כדי להציג את כל המוצרים מקטגוריה זו ועדכון הגריד ויו בהתאם
    {
        if (filters.SelectedIndex == -1)
        {
            chosen = -1;
            return;

        }
        List<int> indexes = new List<int>();
        foreach (ListItem item in filters.Items)
        {
            if (item.Selected == true)
            {
                indexes.Add(filters.Items.IndexOf(item));
            }
        }
        DataTable dt;
        dt = new DataTable();
        foreach (int i in indexes)
        {
            chosen = i;
            dt.Merge(ProductService.GetProductByCategory(chosen + 1));
        }

        products.DataSource = dt;
        products.DataBind();
        chosen = -1;
    }

    protected void src_Click(object sender, EventArgs e) //פעולה שרצה כאשר המשתמש מחפש מוצר שמציגה את פרטי המוצר שהמשתמש חיפש
    {
        showingSrcResults = 1;
        filters.SelectedIndex = -1;
        filters.Visible = false;
        products.DataSource = ProductService.GetProductByName(prodSrc.Text);
        products.DataBind();
    }

    protected void showAll_Click(object sender, EventArgs e) //פעולה שרצה כאשר המשתמש רוצה להראות את כל המוצרים
    {
        showingSrcResults = 0;
        filters.Visible = true;
        prodSrc.Text = null;
        products.DataSource = ProductService.GetAllProducts();
        products.DataBind();
    }
}