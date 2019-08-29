using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Drawing;
using System.IO;
public partial class Users_Basket : System.Web.UI.Page
{
    //משתנים שמשמשים לבדוק באיזה עמוד נמצא המשתמש בגריד ויו והאם הגריד ויו מראה תוצאות חיפוש או לא
    private static int pageIndex = 0;
    private static int totalPrice = 0;

    private static DataTable ProductsInBasket; // משתנה שמאחסן את טבלת המוצרים שבסל הקניות

    //מאכסן את סוג המטבע שבו רואים את הסכום הסופי
    public static string currency;

    //שומר את הערך שבו כופלים את המחיר הסופי של המוצרים (משמש להמרה בין שקלים למטבעות זרים)
    private static double convertTo;

    //פעולה הרצה כאשר המשתמש נכנס לדף
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            pageIndex = 0; //עדכון המשתנה שמכיל את מספר העמוד בגריד ויו בו המשתמש צופה

        updateVariables();         //עדכון המשתנים convertTo וCurrency בהתאם למידע בסשן
        if (!IsPostBack)
            updateBasket();//עדכון הגריד ויו בהתאם למידע השמור בסשן
        else if (Basket.Rows.Count != 0)
            updateFooter();//עדכון הפוטר של הגריד ויו
    }

    //פעולה המתרחשת כאשר נמחקת שורה בסל הקניות (כלומר, פעולה המתרחשת כאשר המשתמש מוחק מוצר מסל הקניות(
    protected void Basket_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //איפוס המחיר הכללי של כל המוצרים בסל
        totalPrice = 0;
        // מחיקת המוצר מהDataTable ProductsInBasket
        ProductsInBasket.Rows.RemoveAt(e.RowIndex);
        //עדכון המידע בסשן
        Session["Basket"] = ProductsInBasket;
        //עדכון הGridView Basket
        Basket.DataSource = ProductsInBasket;
        Basket.DataBind();
        if (((DataTable)Session["Basket"]).Rows.Count == 0)
        {
            Session["Basket"] = null;
            finishOrder.Visible = false;
        }
    }

    //פעולה אשר מעדכנת את סל הקניות בהתאם למידע השמור בסשן
    protected void updateBasket()
    {
        totalPrice = 0;
        //Basket.DataSource = null;
        if (Session["Basket"] != null)
        {
            ProductsInBasket = (DataTable)Session["Basket"];
            Basket.DataSource = ProductsInBasket;
            finishOrder.Visible = true;

            if (((DataTable)Session["Basket"]).Rows.Count == 0)
            {
                Session["Basket"] = null;
                finishOrder.Visible = false;
            }
        }
        Basket.DataBind();
    }

    //פעולה המעדכנת את המשתנים currency וconvertTo בהתאם למידע השמור בסשן
    protected void updateVariables()
    {
        if (Session["Currency"] != null)
            currency = Session["Currency"].ToString();
        else
            currency = "₪";

        if (Session["convertTo"] != null)
            convertTo = double.Parse(Session["convertTo"].ToString());
        else
            convertTo = 1.0;
    }

    //פעולה הרצה כל פעם ששורש בגריד ויו נוצרת
    protected void Basket_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //חישוב המחיר הכללי של כל המוצרים
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int amountRequired = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "AmountRequired"));
            totalPrice += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Price")) * amountRequired;
        }

        //הוספת controls לfooter כדי שיהיה ניתן להמיר בין סוגי מטבעות שונים
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            status.Visible = false;
            //יצירת כפתור של המרה לשקל
            LinkButton shekelButton = new LinkButton();
            shekelButton.Font.Underline = false;
            shekelButton.Text = "₪";
            shekelButton.ForeColor = Color.Gold;
            shekelButton.ID = "shekelButton";

            //יצירת פעולה שתתרחש כאשר כפתור ההמרה לשקל ילחץ
            shekelButton.Click += (object _sender, EventArgs _e) =>
            {
                status.Visible = false;
                Session["convertTo"] = 1.0;
                Session["Currency"] = "₪";
                updateVariables();
                e.Row.Cells[0].Text = "סך הכל:";
                e.Row.Cells[0].Font.Bold = true;
                e.Row.Cells[1].Text = (totalPrice * convertTo).ToString() + " " + currency;
                e.Row.Cells[1].Font.Bold = true;
            };

            //יצירת כפתור המרה לדולר
            LinkButton dollarButton = new LinkButton();
            dollarButton.Font.Underline = false;
            dollarButton.Text = "$";
            dollarButton.ForeColor = Color.Gold;
            dollarButton.ID = "dollarButton";

            //יצירת פעולה שתתרחש כאשר כפתור ההמרה לדולר יילחץ
            dollarButton.Click += (object _sender, EventArgs _e) =>
            {
                try
                {
                    CurrencyConvertorWS.CurrencyConvertorWS client = new CurrencyConvertorWS.CurrencyConvertorWS();
                    double ConversionRate = client.GetConversionRate(CurrencyConvertorWS.Currency.USD);
                    Session["convertTo"] = ConversionRate;
                    Session["Currency"] = "$";
                    updateVariables();
                    e.Row.Cells[0].Text = "סך הכל:";
                    e.Row.Cells[0].Font.Bold = true;
                    e.Row.Cells[1].Text = (totalPrice * convertTo).ToString() + " " + currency;
                    e.Row.Cells[1].Font.Bold = true;
                }
                catch (Exception ex)
                {
                    status.Text = ".שירות ההמרה למטבעות שונים אינו זמין כעת";
                    status.Visible = true;
                }
            };

            //יצירת כפתור המרה לאירו
            LinkButton euroButton = new LinkButton();
            euroButton.Font.Underline = false;
            euroButton.Text = "€";
            euroButton.ForeColor = Color.Gold;
            euroButton.ID = "euroButton";

            //יצירת פעולה שתתרחש כאשר כפתור ההמרה לאירו יילחץ
            euroButton.Click += (object _sender, EventArgs _e) =>
            {
                try
                {
                    CurrencyConvertorWS.CurrencyConvertorWS client = new CurrencyConvertorWS.CurrencyConvertorWS();
                    double ConversionRate = client.GetConversionRate(CurrencyConvertorWS.Currency.EUR);
                    Session["convertTo"] = ConversionRate;
                    Session["Currency"] = "€";
                    updateVariables();
                    e.Row.Cells[0].Text = "סך הכל:";
                    e.Row.Cells[0].Font.Bold = true;
                    e.Row.Cells[1].Text = (totalPrice * convertTo).ToString() + " " + currency;
                    e.Row.Cells[1].Font.Bold = true;
                }

                catch (Exception ex)
                {
                    status.Text = ".שירות ההמרה למטבעות שונים אינו זמין כעת";
                    status.Visible = true;
                }
            };

            //לייבלים שמטרתם לשמש כ"קישוט"
            Label l = new Label();
            l.Text = " | ";
            Label l1 = new Label();
            l1.Text = " | ";

            //הוספת כל הcontrols שנוצרו לfooter
            e.Row.Cells[2].Controls.Add(shekelButton);
            e.Row.Cells[2].Controls.Add(l);
            e.Row.Cells[2].Controls.Add(euroButton);
            e.Row.Cells[2].Controls.Add(l1);
            e.Row.Cells[2].Controls.Add(dollarButton);
            e.Row.Cells[0].Text = "סך הכל:";
            e.Row.Cells[0].Font.Bold = true;
            e.Row.Cells[1].Text = (totalPrice * convertTo).ToString() + " " + currency;
            e.Row.Cells[1].Font.Bold = true;
        }
    }

    //מעדכן את הfooter של הטבלה
    protected void updateFooter()
    {
        status.Visible = false;
        //יצירת כפתור המרה לשקל
        LinkButton shekelButton = new LinkButton();
        shekelButton.Font.Underline = false;
        shekelButton.Text = "₪";
        shekelButton.ForeColor = Color.Gold;
        shekelButton.ID = "shekelButton";

        //יצירת פעולה שתתרחש כאשר כפתור ההמרה לשקל יילחץ
        shekelButton.Click += (object _sender, EventArgs _e) =>
            {
                status.Visible = false;
                Session["convertTo"] = 1.0;
                Session["Currency"] = "₪";
                updateVariables();
                Basket.FooterRow.Cells[0].Text = "סך הכל:";
                Basket.FooterRow.Cells[0].Font.Bold = true;
                Basket.FooterRow.Cells[1].Text = (totalPrice * convertTo).ToString() + " " + currency;
                Basket.FooterRow.Cells[1].Font.Bold = true;
            };

        //יצירת כפתור המרה לדולר
        LinkButton dollarButton = new LinkButton();
        dollarButton.Font.Underline = false;
        dollarButton.Text = "$";
        dollarButton.ForeColor = Color.Gold;
        dollarButton.ID = "dollarButton";

        //יצירת פעולה שתתרחש כאשר כפתור ההמרה לדולר יילחץ
        dollarButton.Click += (object _sender, EventArgs _e) =>
        {
            try
            {
                CurrencyConvertorWS.CurrencyConvertorWS client = new CurrencyConvertorWS.CurrencyConvertorWS();
                double ConversionRate = client.GetConversionRate(CurrencyConvertorWS.Currency.USD);
                Session["convertTo"] = ConversionRate;
                Session["Currency"] = "$";
                updateVariables();
                Basket.FooterRow.Cells[0].Text = "סך הכל:";
                Basket.FooterRow.Cells[0].Font.Bold = true;
                Basket.FooterRow.Cells[1].Text = (totalPrice * convertTo).ToString() + " " + currency;
                Basket.FooterRow.Cells[1].Font.Bold = true;
            }
            catch (Exception ex)
            {
                status.Text = ".שירות ההמרה למטבעות שונים אינו זמין כעת";
                status.Visible = true;
            }
        };

        //יצירת כפתור המרה לאירו
        LinkButton euroButton = new LinkButton();
        euroButton.Font.Underline = false;
        euroButton.Text = "€";
        euroButton.ForeColor = Color.Gold;
        euroButton.ID = "euroButton";

        //יצירת פעולה שתתרחש כאשר כפתור ההמרה לאירו יילחץ
        euroButton.Click += (object _sender, EventArgs _e) =>
        {
            try
            {
                CurrencyConvertorWS.CurrencyConvertorWS client = new CurrencyConvertorWS.CurrencyConvertorWS();
                double ConversionRate = client.GetConversionRate(CurrencyConvertorWS.Currency.EUR);
                Session["convertTo"] = ConversionRate;
                Session["Currency"] = "€";
                updateVariables();
                Basket.FooterRow.Cells[0].Text = "סך הכל:";
                Basket.FooterRow.Cells[0].Font.Bold = true;
                Basket.FooterRow.Cells[1].Text = (totalPrice * convertTo).ToString() + " " + currency;
                Basket.FooterRow.Cells[1].Font.Bold = true;
            }

            catch (Exception ex)
            {
                status.Text = ".שירות ההמרה למטבעות שונים אינו זמין כעת";
                status.Visible = true;
            }
        };

        //לייבלים שמטרתם לשמש כ"קישוט"
        Label l = new Label();
        l.Text = " | ";
        Label l1 = new Label();
        l1.Text = " | ";

        //הוספת כל הcontrols שנוצרו לfooter
        Basket.FooterRow.Cells[2].Controls.Add(shekelButton);
        Basket.FooterRow.Cells[2].Controls.Add(l);
        Basket.FooterRow.Cells[2].Controls.Add(euroButton);
        Basket.FooterRow.Cells[2].Controls.Add(l1);
        Basket.FooterRow.Cells[2].Controls.Add(dollarButton);
        Basket.FooterRow.Cells[0].Text = "סך הכל:";
        Basket.FooterRow.Cells[0].Font.Bold = true;
        Basket.FooterRow.Cells[1].Text = (totalPrice * convertTo).ToString() + " " + currency;
        Basket.FooterRow.Cells[1].Font.Bold = true;
    }

    //פעולה המתרחשת כאשר המשתמש לוחץ על הכפתור "סיים הזמנה"
    protected void finishOrder_Click(object sender, EventArgs e)
    {
        //עובד רק אם המשתמש מחובר
        if (Session["Username"] != null)
            Response.Redirect("FinishOrder.aspx");

        //מציג שגיאה אם המשתמש לא מחובר
        else
            loggedInValidation.IsValid = false;
    }

    //פעולה שמתרחשת כאשר המשתמש מנסה לערוך שורה
    protected void Basket_RowEditing(object sender, GridViewEditEventArgs e)
    {
        Basket.EditIndex = e.NewEditIndex;
        totalPrice = 0;
        Basket.PageIndex = pageIndex;
        Basket.DataSource = ProductsInBasket;
        Basket.DataBind();
        status.Visible = false;
    }

    //פעולה שמתרחשת כאשר המשתמש מעדכן את הכמות המבוקשת ממוצר
    protected void Basket_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int amountRequired = int.Parse(((TextBox)Basket.Rows[e.RowIndex].Cells[5].Controls[0]).Text);
        int ProductID = int.Parse(Basket.Rows[e.RowIndex].Cells[1].Text);
        //בדיקה - האם יש מספיק מהמוצר על מנת שהמשתמש יוכל להזמין את הכמות שהוא מבקש
        if (ProductService.GetStock(ProductID) >= amountRequired)
        {
            ProductsInBasket.Rows[e.RowIndex][ProductsInBasket.Columns[4]] = amountRequired;
            Basket.EditIndex = -1;
            totalPrice = 0;
            Basket.DataSource = ProductsInBasket;
            Basket.DataBind();
            status.Visible = false;
        }
        else
        {
            status.Text = "!אין מספיק מוצרים במלאי. אנא בחר כמות אחרת";
            status.Visible = true;
        }
    }

    //פעולה שמתרחשת כאשר המשתמש מבטל את השינויים שערך
    protected void Basket_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        Basket.EditIndex = -1;
        totalPrice = 0;
        Basket.DataSource = ProductsInBasket;
        Basket.DataBind();
        status.Visible = false;
    }
    //פעולה המתרחשת כאשר המשתמש משנה עמוד (כרגע לא בשימוש)
    protected void Basket_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        totalPrice = 0;
        Basket.PageIndex = e.NewPageIndex;
        pageIndex = e.NewPageIndex;
        Basket.DataSource = ProductsInBasket;
        Basket.DataBind();
    }
}