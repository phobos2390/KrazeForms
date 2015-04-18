using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace KrazeForms
{
    class BoardController
    {
        private static string MAPSAVEFILE = "Maze";
        private static string PLAYERSAVEFILE = "Player Stats";
        private BoardView view;
        private Model model;

        public BoardController(Model model)
        {
            this.model = model;
            this.view = new BoardView(this);
        }

        public BoardController(Model model, int viewHeight, int viewWidth)
        {
            this.model = model;
            this.view = new BoardView(this,viewHeight,viewWidth);
        }

        public BoardView View
        {
            get
            {
                return this.view;
            }
        }

        public void MovePlayer(Direction direction)
        {
            if (this.model.CanMovePlayer(direction))
            {
                this.model.MovePlayer(direction);
            }
        }

        public IDirection GetPlayerDirection()
        {
            return this.model.GetPlayerDirection();
        }

        public Point GetPlayerPosition()
        {
            return this.model.GetPlayerPosition();
        }

        public Point GetSelectedPoint()
        {
            return this.model.GetSelectedPoint();
        }

        public int GetNumberOfKeys()
        {
            return this.model.GetNumberOfKeys();
        }

        public void Save()
        {
            this.model.Save(PLAYERSAVEFILE, MAPSAVEFILE);
        }

        public void InteractWithSpace(Direction dir)
        {
            this.model.InteractWithSpace(dir);
        }

        public ISpace GetSpace(int row, int col)
        {
            return this.model.GetSpace(row, col);
        }

        public int GetRows()
        {
            return this.model.GetRows();
        }

        public int GetColumns()
        {
            return this.model.GetColumns();
        }
    }
}
