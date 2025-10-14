using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestServices
{
    public class Messager
    {
          public string FromAddress { get; set; }
          public string ToAddress { get; set; }
          public string Subject { get; set; }
          public string Content { get; set; }   
          public string GmailAppPassword { get; set; }

    }
}
