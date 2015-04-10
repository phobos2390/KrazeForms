using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace KrazeForms
{
    public class Player
    {
        private int numberOfKeys;
        private System.Drawing.Point playerPosition;
        private bool hasWon;
        private IDirection currentDirection;

        public Player(int numberOfKeys, System.Drawing.Point playerPosition)
        {
            this.numberOfKeys = numberOfKeys;
            this.playerPosition = playerPosition;
            this.currentDirection = DirectionFactory.CreateDirection(Direction.None);
            hasWon = false;
        }

        public Player(System.IO.StreamReader file)
        {
            int positionX = System.Int16.Parse(file.ReadLine());
            int positionY = System.Int16.Parse(file.ReadLine());
            this.playerPosition = new System.Drawing.Point(positionX, positionY);
            this.numberOfKeys = System.Int16.Parse(file.ReadLine());
            this.currentDirection = DirectionFactory.CreateDirection(Direction.None);
            hasWon = false;
        }

        public IDirection CurrentDirection
        {
            get
            {
                return this.currentDirection;
            }
            set
            {
                this.currentDirection = value;
            }
        }

        public int NumberOfKeys
        {
            get
            {
                return numberOfKeys;
            }
            set
            {
                numberOfKeys = value;
            }
        }

        public System.Drawing.Point PlayerPosition
        {
            get
            {
                return playerPosition;
            }
            set
            {
                playerPosition = value;
            }
        }

        public bool HasWon
        {
            get
            {
                return hasWon;
            }
            set
            {
                hasWon = value;
            }
        }

        public System.Drawing.Point JAction()
        {
            return currentDirection.InteractionSpace(playerPosition);
        }
    }
}
