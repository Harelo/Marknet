using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OleDb;
using System.Web.UI;
using System.Web.UI.WebControls;

public class Register
{

    //פעולה אשר רושמת משתמש חדש במסד הנתונים
    public static string MemberRegister(string username, string password, int age, string gender, string fullName, string email, string cp, int CityID, string address, int apartmentNum)
    {
        OleDbConnection conStr = new OleDbConnection(); //יצירת משתנה מסוג חיבור
        conStr.ConnectionString = Connect.getConnectionString(); //השמת מחרוזת חיבור במשתנה מסוג החיבור שיצרנו
        OleDbCommand cmd = new OleDbCommand(); //יתירת משתנה מסוג פקודה
        string stSQL; //יצירת מחרוזת שתכיל פקודת אס קיו אל
        password = TextService.Encrypt(password); //הצפנת הסיסמא שהעוברה בעת הקריאה לפעולה
        //פקודת אס קיו אל שתקפידה להכניס פרטים של משתמש חדש לטבלת המשתמשים במסד הנתונים
        stSQL = "INSERT INTO Accounts (Username, UserPassword, Age, Gender, FullName, Email, Role, City, Address, ApartmentNumber) Values ('" + username + "', '" + password + "', '" + age + "', '" + gender + "', '" + fullName + "', '" + email + "', 'member', "+CityID+", '"+address+"', "+apartmentNum+")";
        cmd.Connection = conStr; //קישור בין המשתנה מסוג הפקודה שיצרנו למשתנה מסוג חיבור שיצרנו
        cmd.CommandText = stSQL; //השמת פקודת האס קיו אל במשתנה מסוג פקודה שיצרנו
        conStr.Open(); //פתיחת החיבור
        cmd.ExecuteNonQuery(); //הרצת הפקודה
        conStr.Close(); //סגירת החיבור
        return "success";
    }
}