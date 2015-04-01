using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace KrazeForms
{
    class BoardView : Panel
    {
        //private int timesInMethod;
        //private int spaceConstant;
        private int widthConstant;
        private int heightConstant;
        private bool showHighlighted;
        private ISpace[,] spaces;
        private Player player;
        private Label keysIndicator;
        private Label NumberOfKeys;
        private System.Collections.ArrayList allKeyCommands;

        //private KeyCommand currentCommand;

        //private Timer timer;

        public BoardView()
        {
            //SpaceFactory.SetImageFactory(new BiggerTextureFactory());
            this.widthConstant = SpaceFactory.GetSpaceWidthConstant();
            this.heightConstant = SpaceFactory.GetSpaceHeightConstant();
            //this.spaceConstant = 10;
            System.IO.StreamReader file = new System.IO.StreamReader("Player Stats");
            this.player = new Player(file);
            file.Close();
            KeyDown += KeyDownEvent;
            KeyPress += Board_KeyPress;
            //timer = new Timer();
            //timer.Interval = 75;
            //timer.Tick += timer_Tick;
            KeyUp += KeyUpEvent;
            player.Moved += PlayerMoveEvent;
            player.Won += PlayerWonEvent;
            LoadSpaces();
            keysIndicator = new Label();
            keysIndicator.Location = new System.Drawing.Point((spaces.GetUpperBound(1) + 1) * this.widthConstant, 10);
            //keysIndicator.Location = new System.Drawing.Point(spaces.GetUpperBound(1) * spaceConstant + 11, 10);
            keysIndicator.Text = "Keys";
            keysIndicator.Height = 15;
            Controls.Add(keysIndicator);
            NumberOfKeys = new Label();
            NumberOfKeys.Location = new System.Drawing.Point((spaces.GetUpperBound(1) + 1) * this.widthConstant + 7, 25);
            //NumberOfKeys.Location = new System.Drawing.Point(spaces.GetUpperBound(1) * spaceConstant + 18, 25);
            NumberOfKeys.Text = "" + player.NumberOfKeys;
            Controls.Add(NumberOfKeys);
            allKeyCommands = new System.Collections.ArrayList();
            allKeyCommands.Add(CommandFactory.Create(Keys.A, Direction.Left));
            allKeyCommands.Add(CommandFactory.Create(Keys.W, Direction.Up));
            allKeyCommands.Add(CommandFactory.Create(Keys.S, Direction.Down));
            allKeyCommands.Add(CommandFactory.Create(Keys.D, Direction.Right));
            allKeyCommands.Add(CommandFactory.Create(Keys.Left, Direction.Left));
            allKeyCommands.Add(CommandFactory.Create(Keys.Up, Direction.Up));
            allKeyCommands.Add(CommandFactory.Create(Keys.Down, Direction.Down));
            allKeyCommands.Add(CommandFactory.Create(Keys.Right, Direction.Right));
            KeyCommand jActionCommand = CommandFactory.Create(Keys.J);
            allKeyCommands.Add(jActionCommand);
            jActionCommand.executed += jCommand_executed;
            KeyCommand escapeCommand = CommandFactory.Create(Keys.Escape);
            allKeyCommands.Add(escapeCommand);
            escapeCommand.executed += escapeCommand_executed;
            KeyCommand hActionCommand = CommandFactory.Create(Keys.H);
            allKeyCommands.Add(hActionCommand);
            hActionCommand.executed += hCommand_executed;
            this.DoubleBuffered = true;
            this.showHighlighted = true;
        }

        //void timer_Tick(object sender, EventArgs e)
        //{
        //    //if (currentCommand != null)
        //    //{
        //    //    player.MovePlayer(currentCommand.DeltaX, currentCommand.DeltaY);
        //    //    currentCommand.ExecuteCommand();
        //    //    this.update();
        //    //}
        //}

        public void Board_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (currentCommand != null)
            //{
            //    player.MovePlayer(currentCommand.DeltaX, currentCommand.DeltaY);
            //    currentCommand.ExecuteCommand();
            //}
        }

        public void escapeCommand_executed(object sender, EventArgs e)
        {
            Save();
            Application.Exit();
        }

        public void jCommand_executed(object sender, EventArgs e)
        {
            System.Drawing.Point checkPoint = player.JAction();
            ISpace toMoveTo = spaces[checkPoint.X, checkPoint.Y];//player.JAction().X, player.JAction().Y];
            toMoveTo.InteractWithPlayer(player);
            NumberOfKeys.Text = "" + player.NumberOfKeys;
        }

        public void hCommand_executed(object sender, EventArgs e)
        {
            this.showHighlighted = !this.showHighlighted;
        }

        public void update()
        {
            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            drawBoard(e.Graphics);
        }

        private void drawBoard(Graphics g)
        {
            for (int i = 0; i <= spaces.GetUpperBound(0); i++)
            {
                for (int j = 0; j <= spaces.GetUpperBound(1); j++)
                {
                    g.DrawImage(spaces[i, j].Texture, j * this.widthConstant, i * this.heightConstant);
//                    g.DrawImage(spaces[i, j].Texture, j * this.spaceConstant, i * this.spaceConstant);
                }
            }
            g.DrawImage(SpaceFactory.CreateImage(SpaceType.Empty), player.PlayerPosition.Y * this.widthConstant, player.PlayerPosition.X * this.heightConstant);
            g.DrawImage(player.Picture, player.PlayerPosition.Y * this.widthConstant, player.PlayerPosition.X * this.heightConstant);
            //g.DrawImage(SpaceFactory.CreateImage(SpaceType.Empty), player.PlayerPosition.Y * this.spaceConstant, player.PlayerPosition.X * this.spaceConstant);
            //g.DrawImage(player.Picture, player.PlayerPosition.Y * this.spaceConstant, player.PlayerPosition.X * this.spaceConstant);
            Point selectedPoint = player.CurrentDirection.InteractionSpace(player.PlayerPosition);
            if (this.showHighlighted)
            {
                g.DrawImage(SpaceFactory.CreateSelectedImage(), selectedPoint.Y * this.widthConstant, selectedPoint.X * this.heightConstant);
            }
            //g.DrawImage(SpaceFactory.CreateSelectedImage(), selectedPoint.Y * this.spaceConstant, selectedPoint.X * this.spaceConstant);
        }

        public void Save()
        {
            System.IO.StreamWriter playerWriter = new System.IO.StreamWriter("Player Stats");
            playerWriter.WriteLine(player.PlayerPosition.X);
            playerWriter.WriteLine(player.PlayerPosition.Y);
            playerWriter.WriteLine(player.NumberOfKeys);
            playerWriter.Close();
            System.IO.StreamWriter fileWriter = new System.IO.StreamWriter("Maze3");
            fileWriter.WriteLine(spaces.GetUpperBound(0) + 1);
            fileWriter.WriteLine(spaces.GetUpperBound(1) + 1);
            for (int i = 0; i < spaces.GetUpperBound(0) + 1; i++)
            {
                int num = 1;
                char prev = spaces[i, 0].FileOutput();
                for (int j = 1; j < spaces.GetUpperBound(1) + 1; j++)
                {
                    char curr = spaces[i, j].FileOutput();
                    if (prev == curr)
                    {
                        num++;
                    }
                    else if (num > 1)
                    {
                        fileWriter.Write("({0}){1}", num, prev);
                        num = 1;
                    }
                    else
                    {
                        fileWriter.Write(prev);
                    }
                    prev = curr;
                }
                if (num > 1)
                {
                    fileWriter.WriteLine("({0}){1}", num, prev);
                }
                else
                {
                    fileWriter.WriteLine(prev);
                }
            }
            fileWriter.Close();
        }

        public ISpace this[int index1, int index2]
        {
            get
            {
                int returnIndex1;
                int returnIndex2;

                if (spaces.GetUpperBound(0) <= index1)
                {
                    returnIndex1 = spaces.GetUpperBound(0) - 1;
                }
                else if (0 > index1)
                {
                    returnIndex1 = 0;
                }
                else
                {
                    returnIndex1 = index1;
                }
                if (spaces.GetUpperBound(1) <= index2)
                {
                    returnIndex2 = spaces.GetUpperBound(1) - 1;
                }
                else if (0 > index2)
                {
                    returnIndex2 = 0;
                }
                else
                {
                    returnIndex2 = index2;
                }
                return spaces[returnIndex1, returnIndex2];
            }
        }

        public void LoadSpaces()
        {
            System.IO.StreamReader fileReader = new System.IO.StreamReader("Maze3");//*/"SaveMatrixVersion2");
            string buffer;
            int index = Int16.Parse(fileReader.ReadLine());
            int index2 = Int16.Parse(fileReader.ReadLine());
            spaces = new ISpace[index, index2];
            Height = index * this.heightConstant;
            Width = index2 * this.widthConstant + 30;
            //Height = index * spaceConstant;
            //Width = index2 * spaceConstant + 30;
            for (int i = 0; i < index; i++)
            {
                buffer = fileReader.ReadLine();
                int colIter = 0;
                for (int j = 0; j < buffer.Length; j++)
                {
                    char check = buffer[j];
                    int num = 0;
                    if (check == '(')
                    {
                        j++;
                        while ('0' <= buffer[j] && buffer[j] <= '9')
                        {
                            num = num * 10 + ((int)buffer[j++] - (int)'0');
                        }
                        j++;
                    }
                    else
                    {
                        num = 1;
                    }
                    for (int k = 0; k < num; k++)
                    {
                        spaces[i, colIter++] = SpaceFactory.CreateSpaceFromChar(buffer[j]);
                    }
                }
            }
            fileReader.Close();
        }

        public void KeyDownEvent(object sender, KeyEventArgs e)
        {
            foreach (KeyCommand command in allKeyCommands)
            {
                if (e.KeyData == command.CommandKey)
                {
                    player.MovePlayer(command.DeltaX, command.DeltaY);
                    command.ExecuteCommand();
                    this.update();
                }
            }
            //this.timesInMethod = 1;
            //timer.Start();
        }

        public void KeyUpEvent(object sender, KeyEventArgs e)
        {
            //currentCommand = null;
            //timer.Stop();
        }

        public void PlayerMoveEvent(object sender, MoveEventArgs e)
        {
            ISpace toMoveTo = spaces[player.PlayerPosition.X + e.MoveX, player.PlayerPosition.Y + e.MoveY];
            if (toMoveTo.PlayerCanMoveIntoSpace(player))
            {
                toMoveTo.InteractWithPlayer(player);
                System.Drawing.Point newPlayerPosition = player.PlayerPosition;
                newPlayerPosition.X += e.MoveX;
                newPlayerPosition.Y += e.MoveY;
                //player.Picture.Visible = false;
                player.PlayerPosition = newPlayerPosition;
                //player.Picture.Visible = true;
                //var p = player.Picture.Location;
                NumberOfKeys.Text = "" + player.NumberOfKeys;
            }
        }

        public void PlayerWonEvent(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
