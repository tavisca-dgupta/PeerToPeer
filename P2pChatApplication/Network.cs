using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace P2pChatApplication
{
    public class Network
    {
        IPEndPoint localEndPoint;

        public Socket GetSocket(string address,int portNumber)
        {
            //IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());

            IPAddress ipAddr = IPAddress.Parse(address);
            localEndPoint = new IPEndPoint(ipAddr, portNumber);
            return new Socket(ipAddr.AddressFamily,
                 SocketType.Stream, ProtocolType.Tcp);
        }
        public void Connect(Socket sender)
        {
            sender.Connect(localEndPoint);
        }

        public void BindSocket(Socket listenerSocket)
        {
            listenerSocket.Bind(localEndPoint);
        }
    }
}
