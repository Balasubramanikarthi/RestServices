using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer1
{
    public abstract class EmailSender
    {
        public string FromAddress;
        public string ToAddress;
        public string Subject;
        public string Content;
        

        public EmailSender(string fromaddress, string toaddress, string subject, string content)
        {
            FromAddress = fromaddress;
            ToAddress = toaddress;
            Subject = subject;
            Content = content;
        }

        public abstract void MessageTransfer();
    }
}

