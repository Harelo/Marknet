using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OleDb;

/// <summary>
/// Summary description for Forgot
/// </summary>
public class ForgotUser
{
    //פעולה שמחזירה את המייל של המשתמש בהתאם לשם המשתמש שלו או למייל שהזין
    public static string ForgotUserOrEmail(string userOrEmail)
    {
        OleDbConnection conStr = new OleDbConnection(); //יצירת חיבור חדש
        conStr.ConnectionString = Connect.getConnectionString(); //השמת מחרוזת החיבור בחיבור שיצרנו
        OleDbCommand cmd = new OleDbCommand(); //יצירת עצם מסוג פקודה חדש
        string strSQL; //יצירת משתנה מסוג מחרוזת חדש שיכיל פקודת אס קיו אל
        strSQL = @"SELECT Accounts.Email
FROM Accounts
WHERE((Accounts.Username)='" + userOrEmail + "')"; //השמת פקודת אס קיו אל בתוך המשתנה שיצרנו שתפקידה למצוא מה המייל של המשתמש ששמו נקבע לפי המשתנה שהעוברו בעת הקריאה לפעולה
        cmd.Connection = conStr; //השמת החיבור שיצרנו בפקודה
        cmd.CommandText = strSQL; //השמת פקודת האס קיו אל שיצרנו בעצם מסוג הפקודה

        conStr.Open(); //פתיחת החיבור
        object obj = cmd.ExecuteScalar(); //הרצת הפקודה והשמת הערך המוחזר ממנה במשתנה
        conStr.Close(); //סגירת החיבור
        if (obj == null) //בדיקה האם יש אימייל שתואם לשם המשתמש שנבדק
        { //אם אין, השורות הבאות רצות

            conStr.ConnectionString = Connect.getConnectionString(); //השמת מחרוזת החיבור בחיבור שיצרנו
            strSQL = @"SELECT Accounts.Email
FROM Accounts
WHERE((Accounts.Email)='" + userOrEmail + "')"; //השמת פקודת אס קיו אל בתוך המשתנה שיצרנו שתפקידה למצוא מה המייל של המשתמש שהאימייל שלו נקבע לפי המשתנה שהעוברו בעת הקריאה לפעולה
            cmd.Connection = conStr; //השמת החיבור שיצרנו בפקודה
            cmd.CommandText = strSQL; //השמת פקודת האס קיו אל שיצרנו בעצם מסוג הפקודה

            conStr.Open(); //פתיחת החיבור
            obj = cmd.ExecuteScalar(); //הרצת הפקודה והשמת הערך המוחזר ממנה במשתנה
            conStr.Close(); //סגירת החיבור
            if (obj == null) //בדיקה האם יש אימייל שתואם לאימייל שהעובר במשתנה בעת הקריאה לפעולה
                return "notFound"; //אם אין אימייל תואם - הפעולה מחזירה שגיאה
            return obj.ToString(); //אם יש אימייל תואם הפעולה מחזירה אותו
        }
        return obj.ToString(); //אם יש אימייל שתואם לשם המשתמש שהעובר במשתנה בעת הקריאה לפעולה הפעולה מחזירה את האימייל ששייך לו
    }

    //פעולה שמחזירה את שם המשתמש והסיסמא של המשתמש בהתאם למייל שלו
    public static string[] getDetailsByEmail(string address)
    {
        string[] details = new string[2] { "Error", "Error" }; //מערך שמכיל את פרטי המשתמש
        OleDbConnection conStr = new OleDbConnection(); //יצירת חיבור חדש
        conStr.ConnectionString = Connect.getConnectionString(); //השמת מחרוזת החיבור בחיבור שיצרנו
        OleDbCommand cmd = new OleDbCommand(); //יצירת עצם מסוג פקודה חדש

        string strSQL;
        strSQL = @"SELECT Accounts.Username
FROM Accounts
WHERE ((Accounts.Email)='" + address + "')"; //פקודת אס קיו אל שתפקידה למצוא שם משתמש לפי אימייל
        cmd.Connection = conStr; //השמת החיבור שיצרנו בפקודה
        cmd.CommandText = strSQL; //השמת פקודת האס קיו אל שיצרנו בעצם מסוג הפקודה
        conStr.Open(); //פתיחת החיבור
        details[0] = cmd.ExecuteScalar().ToString(); //הרצת הפקודה והשמת הערך המוחזר בתא הראשון במערך
        conStr.Close(); //סגירת החיבור

        strSQL = @"SELECT Accounts.UserPassword
FROM Accounts
WHERE ((Accounts.Email)='" + address + "')"; //פקודת אס קיו אל שתפקידה למצוא סיסמא לפי אימייל
        cmd.CommandText = strSQL;
        conStr.Open(); //פתיחת החיבור
        details[1] = TextService.Decrypt(cmd.ExecuteScalar().ToString()); //הרצת הפקודה והשמת הערך המוחזר בתא השני במערך - הערך המוחזר הוא סיסמא שבמסד הנתונים שמורה בצורה מוצפנת, לכן, בנוסף
        //להשמתה אנו גם מפענחים אותה
        conStr.Close(); //סגירת החיבור
        return details; //החזרת המערך
    }
}