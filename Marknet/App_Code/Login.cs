using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OleDb;
using System.Security.Cryptography;
using System.IO;
using System.Text;

public class Login
{
    //פעולת שבודקת האם הסיסמא ושם המשתמש שהמשתמש הזין זהים לאלו ששמורים במסד הנתונים
    public static bool MemberLogin(string username, string password)
    {
        OleDbConnection conStr = new OleDbConnection(); //יצירת משתנה מסוג חיבור

        conStr.ConnectionString = Connect.getConnectionString(); //השמת מחרוזת חיבור במשתנה מסוג החיבור שיצרנו

        OleDbCommand cmd = new OleDbCommand(); //יצירת משתנה מסוג פקודה

        string SQLstr = @"SELECT Accounts.UserPassword
FROM Accounts
WHERE((Accounts.Username)='"+username+"') AND ((Accounts.Activated)=True)"; //יצירת משתנה מסוג מחרוזת שתפקידו להכיל פקודת אס קיו אל שמחזירה סיסמא לפי של משתמש לפי שם המשתמש ובהתאם להאם המשתמש פעיל

        cmd.Connection = conStr; //קישור בין החיבור לפקודה

        cmd.CommandText = SQLstr; //קישור בין מחרוזת שמכילה את הפקודה לעצם מסוג הפקודה

        conStr.Open(); //פתיחת החיבור
        object obj = cmd.ExecuteScalar(); //הרצת הפקודה והשמת הערך המוחזר במשתנה
        conStr.Close(); //סגירת החיבור

        if (obj != null) //בדיקה האם אכן חזר ערך בהרצת הפקודה 
        {
            if (TextService.Decrypt(obj.ToString()) == password) //אם חזר ערך, השורה הבאה בודקת האם הסיסמא שהעוברה בתור משתנה בעת הקריאה לפעולה תואמת לסיסמא שנמצאה בהרצת פקודת האס קיו אל
                return true; //החזרת אמת אם כן
            return false; //החזרת שקר אם לא
        }
        return false; //החזרת שקר אם לא חזר ערך בעת הקריאה לפעולה שמריצה את הפקודה שרשמנו
    }

    //פעולה הבודקת האם טוקן "זכור אותי" מסוים שמור כבר אצל אחד המשתמשים
    public static bool CheckForToken(string token)
    {
        OleDbConnection conStr = new OleDbConnection();
        conStr.ConnectionString = Connect.getConnectionString();
        OleDbCommand cmd = new OleDbCommand();
        string strSQL;
        strSQL = @"SELECT Accounts.Username
FROM Accounts
WHERE((Accounts.RememberMeToken)='" + token + "')";
        cmd.Connection = conStr;
        cmd.CommandText = strSQL;

        conStr.Open();
        object obj = cmd.ExecuteScalar();
        conStr.Close();

        if (obj != null)
            return true;
        return false;
    }

    //פעולה המוסיפה טוקן "זכור אותי" למשתמש - טוקן "זכור אותי" הינו רצף אקראי של תווים
    public static void AddRememberMeTokenToAccount(string username, string token)
    {
        OleDbConnection conStr = new OleDbConnection();
        conStr.ConnectionString = Connect.getConnectionString();
        OleDbCommand cmd = new OleDbCommand();
        string strSQL;
        strSQL = @"UPDATE Accounts SET RememberMeToken = '" + token + "' WHERE Accounts.Username = '" + username + "'";
        cmd.Connection = conStr;
        cmd.CommandText = strSQL;

        conStr.Open();
        cmd.ExecuteScalar();
        conStr.Close();
    }
}