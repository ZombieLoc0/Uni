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
        string color;
        Network connection;

        Game gameController;
        public Form1()
        {
            InitializeComponent();
            Panel[] red = { Rojo1, Rojo2, Rojo3, Rojo4, Rojo5, };
            Panel[] green = { Verde1, Verde2, Verde3, Verde4, Verde5, };
            Panel[] blue = { Azul1, Azul2, Azul3, Azul4, Azul5 };

            gameController = new Game(red, blue, green);
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void RojoButton_Click(object sender, EventArgs e)
        {
            redCounter++;

            if (redCounter >= 3)
            {
                gameController.AddColor("0");
                redCounter = 0;
            }
        }

        private void VerdeButton_Click(object sender, EventArgs e)
        {
            greenCounter++;

            if (greenCounter >= 3)
            {
                gameController.AddColor("1");
                greenCounter = 0;
            }
        }

        private void AzulButton_Click(object sender, EventArgs e)
        {
            blueCounter++;

            if (blueCounter >= 3)
            {
                gameController.AddColor("2");
                blueCounter = 0;
            }
        }

        #region SelectColor
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            color = "0";
        }

        private void checkVerde_CheckedChanged(object sender, EventArgs e)
        {
            color = "1";
        }

        private void checkAzul_CheckedChanged(object sender, EventArgs e)
        {
            color = "2";
        }
        #endregion
       
        private void recieveColor()
        {
            while (true)
            {
                string rcvC = connection.RecieveColor();
                if (color == rcvC) { gameController.RemoveColor(color); }
                else gameController.AddColor(color);
            }
        }

        private void connect_Click(object sender, EventArgs e)
        {
            checkAzul.Enabled = false;
            checkRojo.Enabled = false;
            checkVerde.Enabled = false;

            //Inicar la conexion
            connection = new Network(color);
            Thread recivirAtaque = new Thread(new ThreadStart(recieveColor));
            recivirAtaque.Start();
        }

    }
}
