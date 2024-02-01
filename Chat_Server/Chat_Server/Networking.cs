using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chat_Server
{
    internal class Networking
    {
        const int port = 11000;
        const string ip = "192.168.100.10";
        //UdpClient listener = new UdpClient(port);

        IPEndPoint groupEP = new IPEndPoint(IPAddress.Parse(ip), port);
        Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        Socket conn;
        public Networking()
        {
            sock.Bind(groupEP);
            sock.Listen(1);
            conn = sock.Accept();
        }

        public string Recieve()
        {
            //byte[] bytes = listener.Receive(ref groupEP);
            byte[] bytes = new byte[4096];
            int bSize = conn.Receive(bytes);
            return Encoding.ASCII.GetString(bytes, 0, bSize);
        }

        public void Send(string message)
        {
            byte[] bytes = Encoding.Default.GetBytes(message);
            //listener.Send(bytes, bytes.Length, groupEP);
            conn.Send(bytes);
        }
}
}
