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
        static string ip = "127.0.0.1";
        static int port = 11001;

        public int p1Color;
        public int p2Color;

        IPEndPoint conection = new IPEndPoint(IPAddress.Parse(ip), port);
        Socket initConection = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        Socket player1;
        Socket player2;
        public Network()
        {
            initConection.Bind(conection);
            initConection.Listen(1);
            //
            player1 = initConection.Accept();
            p1Color = int.Parse(RecieveColor(player1));

            player2 = initConection.Accept();
            p2Color = int.Parse(RecieveColor(player2));
        }

        private string RecieveColor(Socket player)
        {
            byte[] data = new byte[1024];
            player.Receive(data);
            return Encoding.ASCII.GetString(data);
        }

        public string recieveP1()
        {
            byte[] data = new byte[2];
            player1.Receive(data);
            return Encoding.ASCII.GetString(data);
        }

        public string recieveP2()
        {
            byte[] data = new byte[2];
            player2.Receive(data);
            return Encoding.ASCII.GetString(data);
        }


        public void SendColor(string color)
        {
            byte[] bytes = Encoding.Default.GetBytes(color);
            player1.Send(bytes);
            player2.Send(bytes);
        }

        public void SendP1(string color)
        {
            byte[] bytes = Encoding.Default.GetBytes(color);
            player1.Send(bytes);
        }

        public void SendP2(string color)
        {
            byte[] bytes = Encoding.Default.GetBytes(color);
            player2.Send(bytes);
        }
    }
}
