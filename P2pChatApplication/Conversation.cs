﻿using System;
using System.Net.Sockets;
using System.Text;

namespace P2pChatApplication
{
    public class Conversation 
    {
        private Display _display;
        public static bool abort;

        public Conversation()
        {
            abort = false;
            _display = new Display();
        }

        public void SendMessage(Socket socket,string message)
        {
            byte[] messageSent = Encoding.ASCII.GetBytes(message);
            int byteSent = socket.Send(messageSent);
            if (message.ToLower().Equals("bye"))
            {
                abort = true;
            }
            
        }
        public void ReceiveMessage(Socket socket)
        {
            byte[] messageReceived = new byte[1024];
            int byteReceived = socket.Receive(messageReceived);
            string data = Encoding.ASCII.GetString(messageReceived, 0, byteReceived);
            _display.DisplayReceivedMessage(data);
            if (data.ToLower().Equals("bye"))
            {
                abort = true;
            }

        }
    
      
    }
}
