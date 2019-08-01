
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2pChatApplication
{
    class ParseUserName
    {
        public int clientPortNumber { set; get; }
        public string clientIp { set; get; }
        public string clientName { set; get; }
        public bool SetIpAddressAndPortNumber(string userName)
        {
            if (userName.Contains("@") || userName.Contains(":"))
            {
                var tempString1 = userName.Split(':');
                var tempString2 = tempString1[0].Split('@');
                clientIp = tempString2[1];
                clientName = tempString2[0];
                clientPortNumber = int.Parse(tempString1[1]);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
