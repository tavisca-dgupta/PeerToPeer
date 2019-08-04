using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace P2pChatApplication
{
    public class NetworkHostConnector
    {
        private Network _network;
        private Socket _listenerSocket;
        private IPEndPoint _localEndPoint;
        private Conversation _conversation;
        private Connection _connection;
        private NetworkHostListener _listener;

        public NetworkHostConnector(Network network,Connection connection)
        {
            _network = network;
            _connection = connection;
            _conversation = new Conversation();
        }
        public void ConnectListener(string address,int portNumber,string clientName)
        {
            _listenerSocket = _network.GetSocket(address,portNumber);
            try
            {
                _network.Connect(_listenerSocket);
                Console.WriteLine($"************{clientName}***********");
                var sendThread=new Thread(_ =>
                {
                    try
                    {
                        while (!(Conversation.abort))
                        {
                            string input;
                            input = Console.ReadLine();
                            _conversation.SendMessage(_listenerSocket, input);
                        }
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine("Your connection is ended");
                    }
                });

                var receiveThread=new Thread(_ =>
                {
                    try
                    {
                        while (!(Conversation.abort))
                        {
                            _conversation.ReceiveMessage(_listenerSocket);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Your connection is ended");
                    }
                });

                sendThread.Start();
                receiveThread.Start();
                
                
               // _connection.CloseConnection(_listenerSocket);
            }
            catch
            {
                Console.WriteLine("Unbale to connect");
            }
        }
      
    }
}
