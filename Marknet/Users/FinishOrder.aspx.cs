using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Threading;
using System.Drawing;
using System.Text;

public partial class Users_FinishOrder : System.Web.UI.Page
{
    private static int totalPrice = 0;
    //מאכסן את סוג המטבע שבו רואים את הסכום הסופי
    public static string currency;
    //שומר את הערך שבו כופלים את המחיר הסופי של המוצרים (משמש להמרה בין שקלים למטבעות זרים)
    private static double convertTo;

    protected void Page_Load(object sender, EventArgs e) //פעולה הרצה כאשר המשתמש נכנס אל הדף
    {
        status.Text = ".שגיאה בביצוע התשלום באמצעות כרטיס האשראי שסופק";
        updateVariables(); //עדכון המשתנים currency וConvertTo בהתאם למידע השמור בSession

        status.Visible = false; //הסתרת השגיאה

        if (Session["Username"] != null)         //בדיקה האם המשתמש מחובר
        {
            //עדכון הgridview של סל הקניות בהתאם למידע השמור בsession
            if (Session["Basket"] != null)
            {
                totalPrice = 0;
                Basket.DataSource = Session["Basket"];
                Basket.DataBind();
            }
            else
                Response.Redirect("Shop.aspx"); //ניתוב המשתמש בחזרה לחנות אם אין לו מוצרים בסל
        }
        else
            Response.Redirect("Login.aspx"); //ניתוב המשתמש לעמוד כניסת משתמש רשום אם הוא אינו מחובר
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

                //הוספת קונטרולים לפוטר כדי שיהיה אפשר להמיר בין סוגי מטבעות שונים
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

            //הוספת כל הקונטרולים שנוצרו לפוטר
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

    protected void payNow_Click(object sender, EventArgs e) //פעולה הרצה כאשר המשתמש לוחץ על הכפתור "שלם עכשיו"
    {
        string[] CardDetails = new string[5];
        CardDetails[0] = CardNumber.Text;
        CardDetails[1] = CSC.Text;
        CardDetails[2] = CardMonth.Text;
        CardDetails[3] = CardYear.Text;
        CardDetails[4] = totalPrice.ToString();

        try
        {
            CreditCardWS.CreditCardWS client = new CreditCardWS.CreditCardWS();
            if (client.UpdateAmount(CardDetails) == "success")
            {
                string orderDate = DateTime.Now.ToString(); //מציאת התאריך והשעה הנוכחים והשמתם במשתנה
                DataTable dt = (DataTable)Session["Basket"]; //השמת סל הקניות במשתנה
                int ProductID, Amount; //משתנים שתפקידם לשמור את כמות המוצר שהמשתמש רוצה לקנות ואת המזהה של המוצר שהמשתמש רוצה לקנות
                string SQLstr = "SELECT ID FROM Accounts WHERE (Username='" + Session["Username"] + "')";
                object result = DBService.GetResultFromSQL(SQLstr); //הרצת פקודה שתפקידה למצוא את מזהה המשתמש המחובר
                int UserID = int.Parse(result.ToString());

                OrderService.InsertIntoOrder(UserID, orderDate); //הכנסת ההזמנה לטבלת ההזמנות

                for (int i = 0; i < dt.Rows.Count; i++) //לולאה שתפקידה להכניס כל אחד ואחד מהמוצרים בסל לטבלת המידע על ההזמנות
                {
                    ProductID = int.Parse(dt.Rows[i][0].ToString());
                    Amount = int.Parse(dt.Rows[i][4].ToString());
                    int currentStock = ProductService.GetStock(ProductID);
                    OrderService.InsertIntoOrderDetails(OrderService.GetNextOrderID(), ProductID, Amount);
                    ProductService.UpdateProductStock(ProductID, currentStock - Amount);
                }
                SendOrderByEmail();
                Session["Basket"] = null; //מחיקת סל הקניות של המשתמש
                Session["PurchaseSuccessful"] = true; //השמה בסשן של אישור הקנייה
                Response.Redirect("Thanks.aspx"); //ניתוב המשתמש לדף תודה על הקנייה
            }

            else
            {
                status.Text = ".שגיאה בביצוע התשלום באמצעות כרטיס האשראי שסופק";
                status.Visible = true;
            }
        }

        catch (Exception ex)
        {
            status.Text = ".לא ניתן לבצע את התשלום כעת";
            status.Visible = true;
        }
    }

    protected void SendOrderByEmail() //פעולה שתפקידה לשלוח מייל למשתמש עם פרטי ההזמנה שלו
    {
        int OrderID = OrderService.GetNextOrderID(); //מציאת המספר המזהה של ההזמנה
        string address = AccountsService.GetEmail(Session["Username"].ToString()); //מציאת כתובת האימייל של המשתמש
        string subject = "Marknet - פרטי הזמנה #" + OrderID; //נושא ההודעה
        string emailMessage = @"
<html lang=""EN"">
<body style =""text-align:left; direction:rtl"">
<center>
<font color=""green"" size=""6"">
Marknet - פרטי הזמנה #" + OrderID + @"
</font>
<br />"; //כתיבת תחילת גוף ההודעה
        StringBuilder builder = new StringBuilder();
        builder.Append("<table align='rtl' border='1px' cellpadding='5' cellspacing='0' style='text-align:right; direction:rtl; border: solid 1px Silver; font-size: medium;'>"); //יצירת טבלה שתשמש כתבלת המידע על ההזמנה ותכיל את פרטי ההזמנה
        builder.Append("<tr valign='top'>");
        for (int i = 0; i < 3; i++) //הוספת כותרות מתאימות לעמודות הטבלה
        {
            builder.Append("<td align='rtl' valign='top'>");
            switch (i)
            {
                case 0:
                    builder.Append("<b>שם מוצר</b>");
                    break;
                case 1:
                    builder.Append("<b>כמות</b>");
                    break;
                case 2:
                    builder.Append("<b>מחיר</b>");
                    break;
            }
            builder.Append("</td>");
        }
        builder.Append("</tr>");
        DataTable MyBasket = new DataTable();
        MyBasket = (DataTable)Session["Basket"]; //הצהרה על משתנה שיכיל את פרטי ההזמנה לפי הסשן
        foreach (DataRow dr in MyBasket.Rows) //כתיבת פרטי ההזמנה בטבלה
        {
            builder.Append("<tr align='rtl' align='right' valign='top'>");
            for (int i = 0; i < 3; i++)
            {
                builder.Append("<td align='rtl' align='right valign='top'>");
                switch (i)
                {
                    case 0:
                        builder.Append(dr[1].ToString());
                        break;
                    case 1:
                        builder.Append(dr[4].ToString());
                        break;
                    case 2:
                        builder.Append(dr[3].ToString());
                        break;
                }
                builder.Append("</td>");
            }
            builder.Append("</tr>");
        }
        builder.Append("</table>");
        emailMessage += "\n" + builder.ToString() + @"<br/><b>מחיר סופי בשקלים</b>: ₪" + totalPrice + @"<br/>המוצרים יהיו בביתך בעוד כ3-5 ימי עסקים.
</center>
</body>
</html>"; //הוספת פרטי ההזמנה להזמנה וסיום ההודעה
        EmailService.SendEmail(address, subject, emailMessage); //שליחת ההודעה
    }
}