using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
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
        public void StartListening()
        {
             _listenerSocket = _network.GetSocket("192.168.1.13");
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
                        _conversation.ReceiveMessage(clientSocket);
                        string input;
                        input = Console.ReadLine();
                        _conversation.SendMessage(clientSocket, input);
                        if (_conversation.NeedToAbort())
                        {
                            break;
                        }
                        else
                            continue;

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
