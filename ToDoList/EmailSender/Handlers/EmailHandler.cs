using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace EmailSender.Handlers
{
    public static class EmailHandler
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
        
    }
}
