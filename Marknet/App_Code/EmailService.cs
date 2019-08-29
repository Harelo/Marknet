using System;
using System.Drawing;
using System.Net.Mail;
using System.Net;
using System.Web;

public class EmailService
{
    public static void SendEmail(string address, string subject, string data) //פעולה אשר שולחת לאדם מסוים מייל
    {
        SmtpClient client = new SmtpClient("smtp.gmail.com", 587) //יצירת הclient לשליחת ההודעה
        {
            Credentials = new NetworkCredential("harelswebsite@gmail.com", "LApFVTN6ZpR5VpK0faxzb6mDCWowjKAMBJDty54HA84csqfWJzrfPprARcthLwT4dbyd5fNGYjZN42agdruX4LyX3ky05wIY8MmH"),
            EnableSsl = true
        };
        MailMessage message = new MailMessage(new MailAddress("harelswebsite@gmail.com", "Marknet"), new MailAddress(address)); //יצירת הודעה חדשה
        message.IsBodyHtml = true; //הוספת הנושא וגוף ההודעה להודעה
        message.Subject = subject; //קביעת נושא ההודעה לפי הנושא שהעובר בעת הקריאה לפעולה
        message.Body = data; //קביעת גוף ההודעה לפי מה שהעובר בעת הקריאה לפעולה
        client.Send(message); //שליחת ההודעה
    }
}