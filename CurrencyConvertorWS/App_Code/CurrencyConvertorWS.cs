using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;

/// <summary>
/// Summary description for CurrencyConvertorWS
/// </summary>
[WebService(Namespace = "http://marknet.com/webservices/currencyconvertor")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class CurrencyConvertorWS : System.Web.Services.WebService
{

    public CurrencyConvertorWS()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    public enum Currency
    {
        USD = 1,
        EUR = 27,

    }

    [WebMethod]
    public double GetConversionRate(Currency currency)
    {
        DataSet tables = new DataSet();
        string XMLAddress = "http://www.boi.org.il/currency.xml?curr=";
        if ((int)currency < 10)
            XMLAddress += "0";
        XMLAddress += (int)currency;
        tables.ReadXml(XMLAddress);
        DataTable CurrencyDetails = tables.Tables[1];
        double ConversionRate = 1 / double.Parse(CurrencyDetails.Rows[0]["RATE"].ToString());
        ConversionRate = double.Parse(String.Format("{0:0.0000}", ConversionRate));
        return ConversionRate;
    }

}
