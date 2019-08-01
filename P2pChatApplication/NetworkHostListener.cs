using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace P2pChatApplication
{
    
    public class NetworkHostListener
    {
        private static Network _network;
        private static Connection _connection;
        private Socket _clientSocket;
        private Socket _listenerSocket;
        private Conversation _conversation;
        public NetworkHostListener(Network network,Connection connection)
        {
            _network = network;
            _connection = connection;
            _conversation = new Conversation();

        }
        public void StartListening(int portNumber)
        {
             _listenerSocket = _network.GetSocket("172.16.5.243",portNumber);
            try
            {
                _network.BindSocket(_listenerSocket);
                _listenerSocket.Listen(10);

                while (true)
                {
                    Console.WriteLine("Waiting connection ... ");
                    Socket clientSocket = _listenerSocket.Accept();
                    while (true)
                    {
                        if (_conversation.NeedToAbort())
                            break;
                        ThreadPool.QueueUserWorkItem(_ =>
                       {
                           _conversation.ReceiveMessage(clientSocket);
                       });

                        ThreadPool.QueueUserWorkItem(_=>
                        {
                            string input;
                            input = Console.ReadLine();
                            _conversation.SendMessage(clientSocket, input);
                        });
                                                
                    }
                    _connection.CloseConnection(clientSocket);

                }

            }

            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }
        public void AcceptConnection()
        {
             _clientSocket = _listenerSocket.Accept();

        }
        public void StopListening()
        {
            _connection.CloseConnection(_listenerSocket);
        }
    }
}
