using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class CommentService
{
    public static void AddComment(string Text, int UserID, int Type) // פעולה שמכניסה תגובה חדשה למסד הנתונים
    {
        DateTime date = DateTime.Now;
        string SQLStr = "INSERT INTO Comments (CommentID, CommentText, UserID, CommentDate, CommentType) VALUES (" + GetNextCommentID() + ", '" + Text + "', " + UserID + ", '" + date + "', " + Type + ")";
        DBService.ExeSQL(SQLStr);
    }

    public static int GetNextCommentID() //פעולה שמחזירה את מזהה התגובה הגבוהה ביותר או אחד אם טבלת התגובות ריקה
    {
        string SQLstr = "SELECT Max(Comments.CommentID) FROM Comments";
        object max = DBService.GetResultFromSQL(SQLstr);
        int IsInt;
        if (!int.TryParse(max.ToString(), out IsInt))
                return 1;
            else
                return int.Parse(max.ToString()) + 1;
    }
}