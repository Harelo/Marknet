using System.Data;
using System.Data.OleDb;

/// <summary>
/// Summary description for DBService
/// </summary>
public class DBService
{

    public static object ExeStoredProcedure(string name)
    {
        OleDbConnection conStr = new OleDbConnection(); //יצירת קונקשיין
        conStr.ConnectionString = Connect.getConnectionString();
        OleDbCommand objCmd = new OleDbCommand(name, conStr); //יצירת עצם מסוג פקודה שמקושר לחיבור שיצרנו ופקודתו היא הפקודה שהעוברה בעת הקריאה לפעולה
        objCmd.CommandType = CommandType.StoredProcedure; //קביעת הפקודה כפקודה מסוג סטורד פרוסיג'ר, משמעו של הדבר הוא שיהיה ניתן למשל להריץ שאילתות
        conStr.Open(); //פתיחת החיבור
        object ret = objCmd.ExecuteScalar(); //הרצת הפקודה והשמת הערך המוחזר במשתנה
        conStr.Close(); //סגירת החיבור
        return ret; //החזרת התוצאה מהפקודה שהורצה
    }

    //פעולה שמחזירה נתונים ממסד הנתונים בצורת דטה טייבל
    public static DataTable SelectFromDB(string SQLstr)
    {
        OleDbConnection myConnection = new OleDbConnection(); //יצירת חיבור חדש

        myConnection.ConnectionString = Connect.getConnectionString(); //השמת מחרוזת החיבור בחיבור שנוצר

        OleDbCommand myCmd = new OleDbCommand(SQLstr, myConnection); //יצירת עצם מסוג פקודה שמקושר לחיבור שיצרנו ופקודתו היא הפקודה שהעוברה בעת הקריאה לפעולה

        OleDbDataAdapter adapter = new OleDbDataAdapter(); //יצירת אדפטר חדש, כלומר, משתנה שיוכל לקרוא מתוך מסד הנתונים

        adapter.SelectCommand = myCmd; //השמת פקודת האדפטר כפקודה שיצרנו קודם

        DataTable dataTable = new DataTable(); //יצירת דטה טייבל חדש
        adapter.Fill(dataTable); //מילוי הדטה טייבל שיצרנו בערכים על פי הפקודה שלוקחת נתונים ממסד הנתונים

        return dataTable; //החזרת הדטה טייבל
    }

    //פעולה שמבצעת פקודת SQL
    public static void ExeScalerSQL(string SQLstr)
    { 
        OleDbConnection myConnection = new OleDbConnection(); //יצירת חיבור חדש

        myConnection.ConnectionString = Connect.getConnectionString(); //השמת מחרוזת החיבור בחיבור שנוצר

        OleDbCommand myCmd = new OleDbCommand(SQLstr, myConnection); //יצירת עצם מסוג פקודה שמקושר לחיבור שיצרנו ופקודתו היא הפקודה שהעוברה בעת הקריאה לפעולה

        myConnection.Open(); //פתיחת החיבור

        myCmd.ExecuteScalar(); //הרצת הפקודה כסקלאר

        myConnection.Close(); //סגירת החיבור
    }

    //פעולה שמבצעת פקודת SQL
    public static void ExeSQL(string SQLstr)
    {
        OleDbConnection myConnection = new OleDbConnection(); //יצירת חיבור חדש

        myConnection.ConnectionString = Connect.getConnectionString(); //השמת מחרוזת החיבור בחיבור שנוצר

        OleDbCommand myCmd = new OleDbCommand(SQLstr, myConnection); //יצירת עצם מסוג פקודה שמקושר לחיבור שיצרנו ופקודתו היא הפקודה שהעוברה בעת הקריאה לפעולה

        myConnection.Open(); //פתיחת החיבור

            myCmd.ExecuteNonQuery(); //הרצת הפקודה כנון קוורי

        myConnection.Close(); //סגירת החיבור
    }

    //פעולה שמבצעת פקודת אס קיו אל ומחזירה את התוצאה
    public static object GetResultFromSQL(string SQLstr)
    {
        OleDbConnection myConnection = new OleDbConnection(); //פתיחת החיבור

        myConnection.ConnectionString = Connect.getConnectionString(); //השמת מחרוזת החיבור בחיבור שנוצר

        OleDbCommand myCmd = new OleDbCommand(SQLstr, myConnection); //יצירת עצם מסוג פקודה שמקושר לחיבור שיצרנו ופקודתו היא הפקודה שהעוברה בעת הקריאה לפעולה

        myConnection.Open(); //פתיחת החיבור

        object obj = myCmd.ExecuteScalar(); //הרצת הפקודה והשמת הערך המוחזר במשתנה

        myConnection.Close(); //סגירת החיבור

        return obj; //החזרת המשתנה שמכיל את הערך המוחזר מהרצת הפקודה
    }
}