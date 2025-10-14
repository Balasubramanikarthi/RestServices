using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer1;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RestServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {

        // POST api/<EmailController>
        [HttpPost]
        public void Post([FromBody] Messager value)
        {

            BuiltInEmailServices mailTransfer = new BuiltInEmailServices(

              value.FromAddress, 
              value.ToAddress,
              value.Subject,
              value.Content,
              value.GmailAppPassword
               );

             mailTransfer.MessageTransfer();

        }

    } 
}
