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

        //private IPAddress GetIpAddress(string address)
        //{
        //    return IPAddress.Parse(address);
        //}
        //public IPEndPoint GetIpEndpoint(string address)
        //{
        //    localEndPoint = new IPEndPoint(GetIpAddress(address), 11111);
        //    return localEndPoint;
        //}
        //public Socket GetSocket(string address)
        //{

        //    return new Socket(GetIpAddress(address).AddressFamily,
        //         SocketType.Stream, ProtocolType.Tcp);
        //}

        public Socket GetSocket(string address)
        {
            //IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());

            IPAddress ipAddr = IPAddress.Parse(address);
            localEndPoint = new IPEndPoint(ipAddr, 11111);
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
