using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Chat_Client
{
    internal class Networking
    {
        const string IP = "192.168.100.10";
        const int port = 11000;

        IPEndPoint ep = new IPEndPoint(IPAddress.Parse(IP), port);
        Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        public Networking()
        {
            sock.Connect(ep);
        }

        public void Send(string message)
        {
            byte[] sendbuff = Encoding.Default.GetBytes(message);
            sock.SendTo(sendbuff, ep);
        }

        public string RecieveMsg()
        {
            byte[] data = new byte[1024];
            int rcv_bites = sock.Receive(data);
            //byte[] data = listener.Receive(ref ep);
            return Encoding.ASCII.GetString(data, 0, rcv_bites);
        }
    }
}
