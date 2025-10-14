using System;
using System.Net;
using System.Net.Mail;

namespace BusinessLayer1
{
    public class BuiltInEmailServices : EmailSender
    {
        
        
            private string GmailAppPassword = null;
            // Constructor for SendAutomatedEmail, passing parameters to the base class
            public BuiltInEmailServices(string fromAddress, string toAddress, string subject, string content, string gmailAppPassword)
                : base(fromAddress, toAddress, subject, content)
            {
                GmailAppPassword = gmailAppPassword;
            }

            // Overriding the SendEmail method to send the email using Gmail SMTP
            public override void MessageTransfer()
            {
                try
                {
                    using (MailMessage mail = new MailMessage())
                    {
                        mail.From = new MailAddress(FromAddress);  // using inherited 'fromAddress'
                        mail.To.Add(ToAddress);  // using inherited 'toAddress'
                        mail.Subject = Subject;  // using inherited 'subject'
                        mail.Body = Content;     // using inherited 'content'
                        mail.IsBodyHtml = true;
                       
                        using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                        {
                            smtp.Credentials = new NetworkCredential(FromAddress, GmailAppPassword);
                            smtp.EnableSsl = true;
                            smtp.UseDefaultCredentials = false;

                            smtp.Send(mail);
                            Console.WriteLine("Email sent successfully!");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Failed to send email: " + ex.Message);
                }
            }
    }
}
