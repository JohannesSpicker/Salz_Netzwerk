using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyMultiPlayerGame
{
    partial class Canvas : UserControl
    {
        Game.Game game;

        public Canvas(Game.Game game)
        {
            InitializeComponent();
            this.game = game;
        }

        private void Canvas_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(Brushes.Blue, new RectangleF(0, 0, 5000, 5000));
            //e.Graphics.DrawLine(Pens.Red, 10, 10, 100, 50);

            this.game.Render(e.Graphics);
        }

        private void Canvas_MouseDown(object sender, MouseEventArgs e)
        {
            this.BeginInvoke(new Action(() =>
            {
                this.game.SpawnSoldier(e.X, e.Y);
            }));
        }
    }
}
