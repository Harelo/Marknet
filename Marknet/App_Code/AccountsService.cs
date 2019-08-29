using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OleDb;
using System.Data;

public class AccountsService
{
    public static string GetEmail(string username)
    {
        string SQLStr = "SELECT Email FROM Accounts WHERE Username='" + username + "'";
        return DBService.GetResultFromSQL(SQLStr).ToString();
    }

    public static DataTable GetDetailsByName(string username) //מחזיר פרטי משתמש לפי שם המשתמש  
    {
        string SQLstr = @"SELECT ID, Username, UserPassword, Age, Gender, FullName, Email, Role, City, Address, ApartmentNumber FROM Accounts WHERE Username LIKE '%" + username + "%'";
        DataTable dt = DBService.SelectFromDB(SQLstr);
        return dt;
    }

    //פעולה אשר בודקת האם שם המשתמש שהמשתמש רוצה להרשם איתו כבר קיים במערכת ומחזירה בהתאם אמת או שקר
    public static bool UsernameExists(string username)
    {
        OleDbConnection conStr = new OleDbConnection();
        conStr.ConnectionString = Connect.getConnectionString();
        OleDbCommand cmd = new OleDbCommand();

        string stSQL;
        stSQL = string.Format(@"SELECT Accounts.Username
FROM Accounts
WHERE ((Accounts.Username)='{0}')", username);
        cmd.Connection = conStr;
        cmd.CommandText = stSQL;
        conStr.Open();

        object obj = cmd.ExecuteScalar();
        conStr.Close();
        if (obj == null)
            return false;
        return true;
    }

    //פעולה אשר בודקת האם האימייל שהמשתמש רוצה להרשם איתו כבר קיים במערכת ומחזירה בהתאם אמת או שקר
    public static bool EmailExists(string email)
    {
        OleDbConnection conStr = new OleDbConnection();
        conStr.ConnectionString = Connect.getConnectionString();
        OleDbCommand cmd = new OleDbCommand();

        string stSQL;
        stSQL = string.Format(@"SELECT Accounts.Email
FROM Accounts
WHERE ((Accounts.Email)='{0}')", email);
        cmd.Connection = conStr;
        cmd.CommandText = stSQL;
        conStr.Open();

        object obj = cmd.ExecuteScalar();
        conStr.Close();
        if (obj == null)
            return false;
        return true;
    }

    //פעולה שמחזירה את התפקיד של המשתמש בהתאם לשם שלו
    public static string GetRole(string username)
    {
        OleDbConnection conStr = new OleDbConnection();
        conStr.ConnectionString = Connect.getConnectionString();
        OleDbCommand cmd = new OleDbCommand();
        string strSQL;
        strSQL = @"SELECT Accounts.Role
FROM Accounts
WHERE((Accounts.Username)='" + username + "')";
        cmd.Connection = conStr;
        cmd.CommandText = strSQL;

        conStr.Open();
        object obj = cmd.ExecuteScalar();
        conStr.Close();
        return obj.ToString();
    }
}