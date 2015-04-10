using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace KrazeForms
{
    class KrazeForm : Form
    {
        private BoardView board;
        private Model model;
        private BoardController controller;

        public KrazeForm()
        {
            Player player = new Player(0, new Point(1, 1));
            MazeGraphBuilder builder = new MazeGraphBuilder();
            builder.SetSize(25, 29);
            builder.CreateGraph();
            Map map = builder.CreateMapFromSpanningTree();
            this.model = new Model(map, player);
            //MapBuilder builder = new MapBuilder();
            //builder.SetHeight(35)
            //    .SetWidth(35);
            //builder.CreateBaseMapPattern(0, 0, 35, 35);
            //builder.SetWallsAroundRectangle(0, 0, 35, 35);
            //builder.CreateWinSpace(34, 23);
            //this.model = new Model(builder.CreateMap(), player);//*/"Maze3","Player Stats");
            this.controller = new BoardController(this.model);
            this.board = this.controller.View;
            this.board.Visible = true;
            this.ClientSize = this.board.Size;
            this.Visible = true;
            this.Name = "Kraze";
            this.Text = "Kraze";
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.Controls.Add(this.board);
            KeyDown += this.KeyDownEvent;
        }

        public void KeyDownEvent(object sender, KeyEventArgs e)
        {
            this.board.KeyDownEvent(sender, e);
            if (this.model.Won)
            {
                Application.Exit();
            }
        }
    }
}
