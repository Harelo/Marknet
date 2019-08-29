using System.Web;

public class Connect
{
    const string fileName = "CreditCardWS.accdb";
    //פעולה המחזירה את מחרוזת החיבור למסד הנתונים
    public static string getConnectionString()
    {
        string location = HttpContext.Current.Server.MapPath("~/App_Data/" + fileName); //מחזיר את מיקום מסד הנתונים
        string connectionString = "provider=Microsoft.ACE.OleDb.12.0;data source=" + location; //מחזיר את מחרוזת ההתחברות
        return connectionString;
    }
}