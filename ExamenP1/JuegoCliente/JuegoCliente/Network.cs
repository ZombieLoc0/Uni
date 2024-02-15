using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using JuegoCliente;

namespace JuegoCliente
{
    internal class Network
    {
        static string ip = "148.239.105.190";
        static int port = 11001;

        IPEndPoint conection = new IPEndPoint(IPAddress.Parse(ip), port);
        Socket realConn = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        public Network(string color)
        {
            realConn.Connect(conection);
            SendColor(color);
        }

        public string RecieveColor()
        {
            byte[] data = new byte[1024];
            int dSize = realConn.Receive(data);
            return Encoding.ASCII.GetString(data);
        }

        public void SendColor(string color)
        {
            byte[] bytes = Encoding.Default.GetBytes(color);
            realConn.Send(bytes);
        }

    }
}
