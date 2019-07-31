using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
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
        public void ConnectListener(string address)
        {
            _listenerSocket = _network.GetSocket(address);
            try
            {
                _network.Connect(_listenerSocket);
                Console.WriteLine(" connected to -> {0} ",
                              _listenerSocket.RemoteEndPoint.ToString());
                while (true)
                {
                    string input;
                    input = Console.ReadLine();
                    _conversation.SendMessage(_listenerSocket, input);
                    _conversation.ReceiveMessage(_listenerSocket);
                    if (_conversation.NeedToAbort())
                        break;
                    else
                        continue;

                }
                _connection.CloseConnection(_listenerSocket);
                _listener.StartListening();
            }
            catch
            {
                Console.WriteLine("Unbale to connect");
            }
        }
    }
}
