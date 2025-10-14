using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace BusinessLayer1
{
    public class MailKitService : EmailSender
    {
        private string GmailAppPassword = null;
        // Constructor for SendAutomatedEmail, passing parameters to the base class
        public MailKitService (string fromAddress, string toAddress, string subject, string content, string gmailAppPassword)
            : base(fromAddress, toAddress, subject, content)
        {
            GmailAppPassword = gmailAppPassword;
        }
        public override void MessageTransfer()
        {
            SendEmail();
        }

        // public void SendEmail()
        public void SendEmail()
        {
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress("Sender Name", FromAddress));
            email.To.Add(new MailboxAddress("Recipient Name", ToAddress));  
            email.Subject = Subject; //"Test Email from C# (MailKit)";
            email.Body = new TextPart("plain", Content);
            //{
            //    Text = "This is a test email sent using MailKit."
            //};

            try
            {
                using (var smtp = new SmtpClient())
                {
                    smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                    smtp.Authenticate(FromAddress, GmailAppPassword);                     
                    smtp.Send(email);
                    smtp.Disconnect(true);
                    Console.WriteLine("Email sent successfully!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to send email: " + ex.Message);
            }
        }
    }
}
