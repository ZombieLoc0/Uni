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
using JuegoServer;

namespace JuegoServer
{

    public partial class Form1 : Form
    {
        int blueCounter = 0;
        int redCounter = 0;
        int greenCounter = 0;

        Game gameController;
        Network net;
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
            net = new Network();
            Thread tP1 = new Thread(new ThreadStart(recieveP1));
            tP1.Start();
            Thread tP2 = new Thread(new ThreadStart(recieveP1));
            tP2.Start();
        }

        private void RojoButton_Click(object sender, EventArgs e)
        {
            redCounter++;

            if(redCounter >= 3)
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

        private void recieveP1()
        {
            while(true)
            {
                string colorRcv = net.recieveP1();
                gameController.AddColor(colorRcv);
            }
        }
        
        private void recieveP2()
        {
            while(true)
            {
                string colorRcv = net.recieveP2();
                gameController.AddColor(colorRcv);
            }
        }
    }
}
