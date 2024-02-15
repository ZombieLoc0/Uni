using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Windows.Forms;

namespace JuegoServer
{
    internal class Network
    {
        static string ip = "148.239.105.190";
        static int port = 11001;

        private string p1Color = "0";
        private string p2Color = "0";

        IPEndPoint conection = new IPEndPoint(IPAddress.Parse(ip), port);
        Socket initConection = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        Socket player1;
        Socket player2;
        public Network()
        {
            initConection.Bind(conection);
            initConection.Listen(1);
            player1 = initConection.Accept();
            p1Color = RecieveColor(player1);

            player2 = initConection.Accept();
            p2Color = RecieveColor(player2);
        }

        private string RecieveColor(Socket player) 
        {
            byte[] data = new byte[1024];
            int dSize = player.Receive(data);
            return Encoding.ASCII.GetString(data);
        }
        public string recieveP1()
        {
            byte[] data = new byte[1024];
            int dSize = player1.Receive(data);
            return Encoding.ASCII.GetString(data);
        }

        public string recieveP2()
        {
            byte[] data = new byte[1024];
            int dSize = player2.Receive(data);
            return Encoding.ASCII.GetString(data);
        }


        public void SendColor(string color)
        {
            byte[] bytes = Encoding.Default.GetBytes(color);
            player1.Send(bytes);
            player2.Send(bytes);
        }

    }
}
