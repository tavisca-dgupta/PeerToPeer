﻿using System;
using System.Text;

namespace P2pChatApplication
{
    public class Display
    {
       
        public void DisplayReceivedMessage(string message)
        {
            Console.WriteLine(message.PadLeft(30));
        }

    }
}
