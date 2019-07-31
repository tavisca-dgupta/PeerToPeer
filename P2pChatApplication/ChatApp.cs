using System;

namespace P2pChatApplication
{
    internal class ChatApp
    {
        private NetworkHostListener _networkHostListener;
        private NetworkHostConnector _networkHostConnector;
        private Network _network;
        private Connection _connection;
        public void StartChat()
        {
            _network = new Network();
            _connection = new Connection();
            _networkHostListener = new NetworkHostListener(_network,_connection);
            _networkHostConnector = new NetworkHostConnector(_network, _connection);
            Console.WriteLine("Hey do you want to start the conversation if yes then press 'Y' else press 'N'");
            var input=Console.ReadLine();
            if(input.ToLower().Equals("y"))
            {
               // _networkHostListener.StopListening();
                _networkHostConnector.ConnectListener("192.168.1.13");
            }
            else
            {
                Console.WriteLine("It seems you didn't press Y so you continues to listen");
                _networkHostListener.StartListening();
            }



        }
    }
}