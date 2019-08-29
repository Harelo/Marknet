using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.OleDb;

public class ConfirmAccount
{
    //פעולה אשר מפעילה משתמש שנמצא במסד הנתונים שמרתחשת כאשר המשתמש לוחץ על הלינק שנשלח לו באימייל לאישור הרישום
    public static bool Activate(string code)
    {
        OleDbConnection conStr = new OleDbConnection();
        conStr.ConnectionString = Connect.getConnectionString();
        OleDbCommand cmd = new OleDbCommand();
        string strSQL;
        strSQL = @"SELECT Accounts.Username
FROM Accounts
WHERE((Accounts.ActivationCode)='" + code + "') AND ((Accounts.Activated)=False)";
        cmd.Connection = conStr;
        cmd.CommandText = strSQL;

        conStr.Open();
        object obj = cmd.ExecuteScalar();
        conStr.Close();
        if (obj != null)
        {
            string username = obj.ToString();
            strSQL = @"UPDATE Accounts SET Activated = True WHERE Username = '" + username + "'";
            cmd.CommandText = strSQL;
            conStr.Open();
            cmd.ExecuteScalar();
            conStr.Close();
            return true;
        }
        return false;
    }

    //פעולה המוסיפה קוד אימות למשתמש - קוד האימות הינו רצף אקראי של תווים
    public static void AddCodeToAccount(string username, string code)
    {
        OleDbConnection conStr = new OleDbConnection();
        conStr.ConnectionString = Connect.getConnectionString();
        OleDbCommand cmd = new OleDbCommand();
        string strSQL;
        strSQL = @"UPDATE Accounts SET ActivationCode = '" + code + "' WHERE Accounts.Username = '" + username + "'";
        cmd.Connection = conStr;
        cmd.CommandText = strSQL;

        conStr.Open();
        cmd.ExecuteScalar();
        conStr.Close();
    }

    //פעולה שמתרחשת כאשר המשתמש לוחץ על לינק האימות שנשלח לו במייל לאחר ההרשמה ותקפידה למצוא מיהו המשתמש אליו שייך הקוד
    public static bool CheckForCode(string code)
    {
        OleDbConnection conStr = new OleDbConnection();
        conStr.ConnectionString = Connect.getConnectionString();
        OleDbCommand cmd = new OleDbCommand();
        string strSQL;
        strSQL = @"SELECT Accounts.Username
FROM Accounts
WHERE((Accounts.ActivationCode)='" + code + "')";
        cmd.Connection = conStr;
        cmd.CommandText = strSQL;

        conStr.Open();
        object obj = cmd.ExecuteScalar();
        conStr.Close();

        if (obj != null)
            return true;
        return false;
    }
}