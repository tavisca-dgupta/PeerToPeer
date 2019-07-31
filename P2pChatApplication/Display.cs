using System;
using System.Text;

namespace P2pChatApplication
{
    public class Display
    {
        public void DisplaySentMessage(string message)
        {
            Console.WriteLine("Text received -> {0} ", message);
        }
        public void DisplayReceivedMessage(string message)
        {
            Console.WriteLine(message.PadLeft(30));
        }

    }
}
