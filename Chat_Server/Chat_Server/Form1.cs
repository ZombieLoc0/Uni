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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static Chat_Server.Networking;

namespace Chat_Server
{
    public partial class Form1 : Form
    {
        Networking serverConn = new Networking();
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
                string msg = serverConn.Recieve();
                ShowReceivedMessage(msg);
            }
        }

        private void sendButton_Click(object sender, EventArgs e)
        {
            serverConn.Send(msgBox.Text);
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
    }
}
