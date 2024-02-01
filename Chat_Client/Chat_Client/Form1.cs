using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Chat_Client.Networking;

namespace Chat_Client
{
    public partial class Form1 : Form
    {
        Networking clientConn = new Networking();
        public Form1()
        {
            InitializeComponent();
            Thread t = new Thread(new ThreadStart(recieve_msg));
            t.Start();
        }

        private void recieve_msg()
        {
            while (true)
            {
                string msg = clientConn.RecieveMsg();
                ShowReceivedMessage(msg);
            }
        }

        private void ShowReceivedMessage(string message)
        {
            if (chatBox.InvokeRequired)
            {
                // Si estamos en un hilo diferente, invoca el método en el hilo de la interfaz de usuario.
                chatBox.BeginInvoke(new Action(() => ShowReceivedMessage(message)));
            }
            else
            {
                // Estamos en el hilo de la interfaz de usuario, puedes actualizar el control directamente.
                chatBox.AppendText("Respuesta: " + message + Environment.NewLine);
            }
        }

        private void sendButton_Click_1(object sender, EventArgs e)
        {
            clientConn.Send(msgBox.Text);
        }
    }
}
