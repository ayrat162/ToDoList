using System;
using System.IO;
using System.Net.Mail;

namespace EmailService
{
    public static class EmailService
    {
        public static bool SendMessage(string text, string emailFrom = "Airat.Musin.GDC@ts.fujitsu.com", string emailTo = "Airat.Musin.GDC@ts.fujitsu.com", string subject = "Test Email", string attachPath=null)
        {
            var client = new SmtpClient();
            var message = new MailMessage(emailFrom, emailTo, subject, text) {IsBodyHtml = true};
            if (attachPath != null)
            {
                var attachment = new Attachment(attachPath);
                message.Attachments.Add(attachment);
            }

            try
            {
                client.Send(message);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static bool SendFakeMessage(string text, string emailFrom = "Airat.Musin.GDC@ts.fujitsu.com", string emailTo = "Airat.Musin.GDC@ts.fujitsu.com", string subject = "Test Email", string attachPath = null)
        {
            File.AppendAllText("log.txt",text);
            return true;
        }
    }
}
