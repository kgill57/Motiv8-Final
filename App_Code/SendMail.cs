using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
namespace MailScheduler
{
    public class SendMail
    {
        public void SendEmail()
        {
            string AppLocation = "";
            AppLocation = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase);
            AppLocation = AppLocation.Replace("file:\\", "");
            string file = AppLocation + "\\ExcelFiles\\DataFile.xlsx";

            var fromAddress = new MailAddress("sdbasketball96@aol.com", "Elk Logistics");
            var toAddress = new MailAddress("mcgillkd@dukes.jmu.edu", "Administrator");
            System.Net.Mail.Attachment attachment = new System.Net.Mail.Attachment(file);
            const string fromPassword = "Daisydoo#1pet";
            const string subject = "New Report Received";
            const string body = "Dear Administrator, here is the report you requested.";

            var smtp = new SmtpClient
            {
                Host = "smtp.aol.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };

            var message = new MailMessage("sdbasketball96@aol.com", "mcgillkd@dukes.jmu.edu", subject, body);
            message.Attachments.Add(attachment);
            smtp.Send(message);
        }
    }
}