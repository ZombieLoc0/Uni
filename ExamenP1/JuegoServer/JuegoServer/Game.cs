using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JuegoServer
{
    internal class Game
    {
        private Panel[] red;
        private Panel[] green;
        private Panel[] blue;

        int redIndex = 0;
        int greenIndex = 0;
        int blueIndex = 0;

        public Game(Panel[] red, Panel[] blue, Panel[] green) 
        {
            this.red = red;
            this.blue = blue;
            this.green = green;
        }

        public void AddColor(int color)
        {
            switch (color)
            {
                case 0:
                    this.red[redIndex].BackColor = Color.Red;
                    redIndex++;
                    if (redIndex > 4) redIndex = 4;
                    break;
                case 1:
                    this.green[greenIndex].BackColor = Color.Green;
                    greenIndex++;
                    if (greenIndex > 4) greenIndex = 4;
                    break;
                case 2:
                    this.blue[blueIndex].BackColor = Color.Blue;
                    blueIndex++;
                    if (blueIndex > 4) blueIndex = 4;
                    break;
            }
        }

        public void RemoveColor(int color)
        {
            switch (color)
            {
                case 0:
                    this.red[redIndex].BackColor = Color.Gray;
                    redIndex--;
                    if (redIndex < 0) redIndex = 0;
                    break;
                case 1:
                    this.green[greenIndex].BackColor = Color.Gray;
                    greenIndex--;
                    if (greenIndex < 0) greenIndex = 0;
                    break;
                case 2:
                    this.blue[blueIndex].BackColor = Color.Gray;
                    blueIndex--;
                    if (blueIndex < 0) blueIndex = 0;
                    break;
            }
        }

    }
}
