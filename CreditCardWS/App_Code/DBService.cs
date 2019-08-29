using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OleDb;
using System.Data;

public class DBService
{
    //פעולה שמבצעת פקודת SQL
    public static void ExeSQL(string SQLstr)
    {
        OleDbConnection myConnection = new OleDbConnection();

        myConnection.ConnectionString = Connect.getConnectionString();

        OleDbCommand myCmd = new OleDbCommand(SQLstr, myConnection);

        myConnection.Open();

        myCmd.ExecuteNonQuery();

        myConnection.Close();
    }

    //פעולה שמחזירה נתונים ממסד הנתונים בצורת דטה טייבל
    public static DataTable SelectFromDB(string SQLstr)
    {
        OleDbConnection myConnection = new OleDbConnection();

        myConnection.ConnectionString = Connect.getConnectionString();

        OleDbCommand myCmd = new OleDbCommand(SQLstr, myConnection);

        OleDbDataAdapter adapter = new OleDbDataAdapter();

        adapter.SelectCommand = myCmd;

        DataTable dataTable = new DataTable();
        adapter.Fill(dataTable);

        return dataTable;
    }

    //פעולה שמבצעת פקודת אס קיו אל ומחזירה את התוצאה
    public static object GetResultFromSQL(string SQLstr)
    {
        OleDbConnection myConnection = new OleDbConnection();

        myConnection.ConnectionString = Connect.getConnectionString();

        OleDbCommand myCmd = new OleDbCommand(SQLstr, myConnection);

        myConnection.Open();

        object obj = myCmd.ExecuteScalar();

        myConnection.Close();

        return obj;
    }
}