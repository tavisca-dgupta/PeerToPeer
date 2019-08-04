using System;
using System.Threading;

namespace P2pChatApplication
{
    internal class ChatApp
    {
        private NetworkHostListener _networkHostListener;
        private NetworkHostConnector _networkHostConnector;
        private Network _network;
        private ParseUserName _userNameparser;
        private Connection _connection;
        
        public void StartChat()
        {
            _network = new Network();
            _connection = new Connection();
            _networkHostListener = new NetworkHostListener(_network,_connection);
            _networkHostConnector = new NetworkHostConnector(_network, _connection);
            _userNameparser = new ParseUserName();
            Console.WriteLine("****************CHAT APP*****************");
            Console.WriteLine("Enter the port in which you want to start the connection");
            string portNo = Console.ReadLine();
            int portNumber = int.Parse(portNo);
            //var startListeningThread = new Thread(_ =>
            //{
            //    _networkHostListener.StartListening(portNumber);
               
            //});

            //startListeningThread.Start();


            Thread listenerThread = new Thread(new ThreadStart(() => _networkHostListener.StartListening(portNumber)));
            listenerThread.Start();

            Console.WriteLine("Hey do you want to start the conversation if yes then press 'Y' else press 'N'");
            var input=Console.ReadLine();
            if(input.ToLower().Equals("y"))
            {
                Console.WriteLine("Enter the username and port in which you want to connect with");
                Console.WriteLine("eg: xyz@'ipofxyz':port number");
                string userName = Console.ReadLine();
                if (_userNameparser.SetIpAddressAndPortNumber(userName))
                {
                    _networkHostConnector.ConnectListener(_userNameparser.clientIp, _userNameparser.clientPortNumber, _userNameparser.clientName);
                }
                else
                    Console.WriteLine("oopss it seems you didn't wrote in correct format");
            }
            else
            {
                Console.WriteLine("It seems you didn't press Y so you continues to listen");
            }

        }
        

    }
}