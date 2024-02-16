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
using JuegoCliente;

namespace JuegoCliente
{

    public partial class Form1 : Form
    {
        int blueCounter = 0;
        int redCounter = 0;
        int greenCounter = 0;
        int iColor;

        Network server;

        Game gameController;
        public Form1()
        {
            InitializeComponent();
            Panel[] red = { Rojo1, Rojo2, Rojo3, Rojo4, Rojo5, };
            Panel[] green = { Verde1, Verde2, Verde3, Verde4, Verde5, };
            Panel[] blue = { Azul1, Azul2, Azul3, Azul4, Azul5 };

            gameController = new Game(red, blue, green);    
        }

        private void ShowDebug(string message)
        {
            if (debugBox.InvokeRequired)
            {
                // Si estamos en un hilo diferente, invoca el método en el hilo de la interfaz de usuario.
                debugBox.BeginInvoke(new Action(() => ShowDebug(message)));
            }
            else
            {
                // Estamos en el hilo de la interfaz de usuario, puedes actualizar el control directamente.
                debugBox.AppendText("Respuesta: " + message + '\n');
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        #region Buttons

        private void RojoButton_Click(object sender, EventArgs e)
        {
            redCounter++;

            if (redCounter >= 3)
            {
                if (iColor != 0) gameController.RemoveColor(0);

                else gameController.AddColor(0);

                server.SendColor(0.ToString());
                redCounter = 0;
            }
        }

        private void VerdeButton_Click(object sender, EventArgs e)
        {
            greenCounter++;

            if (greenCounter >= 3)
            {
                if (iColor != 1) gameController.RemoveColor(1);

                else gameController.AddColor(1);

                server.SendColor(1.ToString());
                greenCounter = 0;
            }
        }

        private void AzulButton_Click(object sender, EventArgs e)
        {
            blueCounter++;

            if (blueCounter >= 3)
            {
                if (iColor != 2) gameController.RemoveColor(2);

                else gameController.AddColor(2);

                server.SendColor(2.ToString());
                blueCounter = 0;
            }
        }

        #endregion

        #region SelectColor
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            iColor = 0;
        }

        private void checkVerde_CheckedChanged(object sender, EventArgs e)
        {
            iColor = 1;
        }

        private void checkAzul_CheckedChanged(object sender, EventArgs e)
        {
            iColor = 2;
        }
        #endregion
       
        private void recieveColor()
        {
            while (true)
            {
                string msg = server.RecieveColor();
                
                if (iColor == int.Parse(msg)) { gameController.RemoveColor(iColor); }
                else gameController.AddColor(int.Parse(msg));
                //ShowDebug(msg);
            }
        }

        private void connect_Click(object sender, EventArgs e)
        {
            checkAzul.Enabled = false;
            checkRojo.Enabled = false;
            checkVerde.Enabled = false;

            AzulButton.Enabled = true;
            RojoButton.Enabled = true;
            VerdeButton.Enabled = true;

            //Inicar la conexion
            server = new Network(iColor.ToString());
            Thread recivirAtaque = new Thread(new ThreadStart(recieveColor));
            recivirAtaque.Start();
        }
    }
}
