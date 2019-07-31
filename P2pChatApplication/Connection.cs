using System.Net.Sockets;

namespace P2pChatApplication
{
    public class Connection
    {
        public void CloseConnection(Socket socket)
        {
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
        }
        
    }
}
