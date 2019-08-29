<%@ Application Language="C#" %>
<script RunAt="server">

    void Application_Start(object sender, EventArgs e)
    {
        // Code that runs on application startup
    }

    void Application_End(object sender, EventArgs e)
    {
        //  Code that runs on application shutdown

    }

    void Application_Error(object sender, EventArgs e) //קוד שרץ כאשר יש שגיאה בריצה של דף כלשהוא באתר
    {
        Exception ex = Server.GetLastError(); //מציאת השגיאה האחרונה שהתרחשה בשרת
        if (ex is HttpException && ((HttpException)ex).GetHttpCode() == 404) //בדיקה האם השגיאה שהתרחשה היא מסוג אייץ' טי טי פי והםא קוד השגיאה הוא 404 (כלומר, דף אינו נמצא)
        {
            Response.Redirect("~/Error.aspx"); //אם השגיאה היא אכן שגיאה של "דף לא נמצא" - שורת הקוד הבאה מעבירה את המשתמש לדף "דף לא נמצא"
        }
    }

    void Session_Start(object sender, EventArgs e) //קוד שרץ כאשר מתחיל סשן חדש
    {
        if (Request.Cookies["rme_token"] != null) //בדיקה האם קיימת עוגייה שכותרתה rme_token
        {
            string token = Request.Cookies["rme_token"].Value; //מציאת הטוקן 
            if (token != null)
            {
                object username = DBService.GetResultFromSQL("SELECT Username FROM Accounts WHERE RememberMeToken='" + token + "'"); //מציאת המשתמש שלו שייך הטוקן
                try //ניסיון לשמור בסשן את שם המשתמש והתפקיד, אם שם המשתמש  אליו שייך התוקן לא נמצא במסד הנתונים מורץ קוד אחר
                {
                    Session["Username"] = username; //אם קיימת עוגייה כזאת, קטע הקוד הבא מעדכן סשן של שם משתמש ותפקיד משתמש בהתאם (ראשית מפענחים את הצפנת שם המשתמש)
                    Session["Role"] = AccountsService.GetRole(Session["Username"].ToString());
                }
                catch (Exception ex) //קוד שמורץ במקרה של שגיאה
                {
                    if(username == null) //בדיקה האם לא נמצא משתמש אליו שייך הטוקן מהעוגייה - אם כך הדבר, השורות הבאות רצות
                    {
                        HttpCookie cookie = new HttpCookie("rme_token"); //אם קיימת עוגייה שכותרתה מרקנט - קטע הקוד הבא מוחק אותה
                        cookie.Expires = DateTime.Now.AddDays(-1); //השמת תאריך התפוגה של העוגייה ליום לפני היום הנוכחי כך שהעוגייה תמחק
                        Response.Cookies.Add(cookie); //שמירת העוגייה
                    }
                }
            }
        }
    }

    void Session_End(object sender, EventArgs e)
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }
       
</script>
