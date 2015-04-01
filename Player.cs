using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace KrazeForms
{
    class Player
    {
        private IDictionary<AbstractDirection, Image> playerTextures = new Dictionary<AbstractDirection,Image>();
        private int numberOfKeys;
        private System.Drawing.Point playerPosition;
        //private Image picture;
        private bool hasWon;
        private IDirection currentDirection;
        private System.Collections.Generic.List<IDirection> allDirections;

        public event MoveEventHandler Moved;
        public event WinEventHandler Won;

        public Player(int numberOfKeys, System.Drawing.Point playerPosition, Image picture)
        {
            this.numberOfKeys = numberOfKeys;
            this.playerPosition = playerPosition;
            //this.picture = picture;
            allDirections = new System.Collections.Generic.List<IDirection>();
            
            foreach (Direction dir in Enum.GetValues(typeof(Direction)))
            {
                IDirection direction = DirectionFactory.CreateDirection(dir);
                allDirections.Add(direction);
                playerTextures.Add((AbstractDirection)direction, SpaceFactory.CreatePlayerImage(direction));
            }
            
            this.currentDirection = new NoDirection();
            this.playerTextures.Add((AbstractDirection)this.currentDirection, SpaceFactory.CreatePlayerImage(this.currentDirection));
            hasWon = false;
        }

        public Player(System.IO.StreamReader file)
        {
            int positionX = System.Int16.Parse(file.ReadLine());
            int positionY = System.Int16.Parse(file.ReadLine());
            this.playerPosition = new System.Drawing.Point(positionX, positionY);
            this.numberOfKeys = System.Int16.Parse(file.ReadLine());
            //this.picture = (System.Drawing.Bitmap)System.Drawing.Image.FromFile("Main Character Tile.png");
            
            allDirections = new System.Collections.Generic.List<IDirection>();

            foreach (Direction dir in Enum.GetValues(typeof(Direction)))
            {
                IDirection direction = DirectionFactory.CreateDirection(dir);
                allDirections.Add(direction);
                playerTextures.Add((AbstractDirection)direction, SpaceFactory.CreatePlayerImage(direction));
            }

            this.currentDirection = new NoDirection();
            this.playerTextures.Add((AbstractDirection)this.currentDirection, SpaceFactory.CreatePlayerImage(this.currentDirection));
            hasWon = false;
        }

        public IDirection CurrentDirection
        {
            get
            {
                return this.currentDirection;
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

        public Image Picture
        {
            get
            {
                return this.playerTextures[(AbstractDirection)this.CurrentDirection];
                //return picture;
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
                Won(this, new EventArgs());
            }
        }

        public System.Drawing.Point JAction()
        {
            return currentDirection.InteractionSpace(playerPosition);
        }

        public void MovePlayer(int x, int y)
        {
            if (Moved != null)
            {
                foreach (IDirection d in allDirections)
                {
                    if (d.MovesInThatDirection(x, y))
                    {
                        currentDirection = d;
                    }
                }
                MoveEventArgs args = new MoveEventArgs(y, x);
                Moved(this, args);
            }
        }
    }
}
