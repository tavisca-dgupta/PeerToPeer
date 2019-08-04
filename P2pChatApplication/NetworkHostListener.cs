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
                Console.WriteLine("Waiting connection ... ");
                Socket clientSocket = _listenerSocket.Accept();
                startConversation(clientSocket);
              
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

        public void startConversation(Socket clientSocket)
        {
            var sendThread = new Thread(_ =>
            {
                try
                {
                    while (!(Conversation.abort))
                    {
                        string input;
                        input = Console.ReadLine();
                        _conversation.SendMessage(clientSocket, input);
                    }
                    _connection.CloseConnection(clientSocket);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Your connection is ended");
                }
            });

            var receiveThread = new Thread(_ =>
            {
                try
                {
                    while (!(Conversation.abort))
                    {
                        _conversation.ReceiveMessage(clientSocket);
                    }
                    _connection.CloseConnection(clientSocket);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Your connection is ended");
                }
            });
            receiveThread.Start();
            sendThread.Start();
            
            
        }
    }
}
