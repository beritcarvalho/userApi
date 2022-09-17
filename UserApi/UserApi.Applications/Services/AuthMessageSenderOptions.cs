using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserApi.Applications.Services
{
    public class AuthMessageSenderOptions
    {
        public string SendInBlueUser { get; set; }
        public string SendInBlueKey { get; set; }
        public string Domain { get; set; }
        public int Port { get; set; }
        public string FromName { get; set; }
        public string FromEmail { get; set; }
    }
}
