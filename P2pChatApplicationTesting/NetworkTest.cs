using P2pChatApplication;
using System;
using System.Net;
using System.Net.Sockets;
using Xunit;

namespace P2pChatApplicationTesting
{
    public class NetworkTest
    {
        [Fact]
        public void Test_Creating_Socket()
        {
            //arrange
            Network network = new Network();

            //act
            IPAddress ipAddr = IPAddress.Parse("172.16.5.243");
            IPEndPoint localEndPoint = new IPEndPoint(ipAddr, 11111);
            Socket socket= new Socket(ipAddr.AddressFamily,
                 SocketType.Stream, ProtocolType.Tcp);

            //assert
            Assert.Equal(socket, network.GetSocket("172.16.5.243", 11111));
        }
    }
}
