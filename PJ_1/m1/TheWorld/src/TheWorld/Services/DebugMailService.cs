using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheWorld.Services
{
    public class DebugMailService : IMailServices
    {
        public void SendMail(string to, string from, string subject, string body)
        {
           // Debug.
        }
    }
}
