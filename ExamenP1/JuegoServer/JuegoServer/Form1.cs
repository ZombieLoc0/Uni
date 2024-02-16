﻿using System;
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

        int iColor;

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

        #region Buttons

        private void RojoButton_Click(object sender, EventArgs e)
        {
            redCounter++;

            if (redCounter >= 3)
            {
                if (iColor != 0) gameController.RemoveColor(0);

                else gameController.AddColor(0);


                net.SendColor(0.ToString());
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

                net.SendColor(1.ToString());
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

                net.SendColor(2.ToString());
                blueCounter = 0;
            }
        }

        #endregion

        #region SelectColor
        private void checkRojo_CheckedChanged(object sender, EventArgs e)
        {
            iColor = 0;
        }

        private void checkVerde_CheckedChanged_1(object sender, EventArgs e)
        {
            iColor = 1;
        }

        private void checkAzul_CheckedChanged_1(object sender, EventArgs e)
        {
            iColor = 2;
        }

        #endregion
        private void recieveP1()
        {
            while(true)
            {
                string colorRcv = net.recieveP1();
                ShowDebug(colorRcv);
                if(int.Parse(colorRcv) == net.p2Color || int.Parse(colorRcv) == iColor) gameController.RemoveColor(int.Parse(colorRcv));
                else gameController.AddColor(int.Parse(colorRcv));

                net.SendP2(colorRcv);
            }
        }
        
        private void recieveP2()
        {
            while(true)
            {
                string colorRcv = net.recieveP2();
                ShowDebug(colorRcv);
                if (int.Parse(colorRcv) == net.p1Color || int.Parse(colorRcv) == iColor) gameController.RemoveColor(int.Parse(colorRcv));
                else gameController.AddColor(int.Parse(colorRcv));
                net.SendP1(colorRcv);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            checkAzul.Enabled = false;
            checkRojo.Enabled = false;
            checkVerde.Enabled = false;

            AzulButton.Enabled = true;
            RojoButton.Enabled = true;
            VerdeButton.Enabled = true;

            Thread tP1 = new Thread(new ThreadStart(recieveP1));
            tP1.Start();
            Thread tP2 = new Thread(new ThreadStart(recieveP2));
            tP2.Start();
        }
    }
}
