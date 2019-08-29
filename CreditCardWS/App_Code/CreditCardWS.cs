using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;

/// <summary>
/// Summary description for CreditCardWS
/// </summary>
[WebService(Namespace = "http://marknet.com/webservices/creditcard")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class CreditCardWS : System.Web.Services.WebService
{

    public CreditCardWS()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string UpdateAmount(string[] CardDetails)
    {
        DataTable dt = DBService.SelectFromDB("SELECT CardNumber, CSC, CardMonth, CardYear FROM Cards WHERE CardNumber=" + CardDetails[0]);
        if (dt.Rows.Count != 0 && dt.Rows[0][0].ToString() == CardDetails[0])
        {
            for (int i = 1; i < dt.Columns.Count; i++)
            {
                string CompareTo = dt.Rows[0][i].ToString();
                if (CardDetails[i] != CompareTo)
                    return "error";
            }

            int amountInCard = int.Parse(DBService.GetResultFromSQL("SELECT AmountInCard FROM Cards WHERE CardNumber=" + CardDetails[0]).ToString());

            if (int.Parse(CardDetails[4]) <= amountInCard)
            {
                int NewAmount = amountInCard - int.Parse(CardDetails[4]);
                DBService.ExeSQL("UPDATE Cards SET AmountInCard=" + NewAmount + " WHERE CardNumber=" + CardDetails[0]);
                return "success";
            }
        }
        return "error";
    }

}
