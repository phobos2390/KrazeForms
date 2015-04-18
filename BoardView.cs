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
        private int widthConstant;
        private int heightConstant;
        private int viewHeight;
        private int viewWidth;
        private bool showHighlighted;
        private Label keysIndicator;
        private Label NumberOfKeys;
        private IList<KeyCommand> allKeyCommands;
        private BoardController controller;
        private IDirection lastDirection;

        public BoardView(BoardController controller, int viewHeight, int viewWidth)
        {
            //SpaceFactory.SetImageFactory(new BiggerTextureFactory());
            this.controller = controller;
            KeyDown += KeyDownEvent;
            this.initSize(viewHeight,viewWidth);
            this.initKeyLabel();
            this.initCommands();
            this.DoubleBuffered = true;
            this.showHighlighted = true;
        }

        public BoardView(BoardController controller)
            :this(controller, controller.GetRows(), controller.GetColumns()){}

        private void initSize(int viewHeight, int viewWidth)
        {
            this.widthConstant = SpaceFactory.GetSpaceWidthConstant();
            this.heightConstant = SpaceFactory.GetSpaceHeightConstant();

            this.viewHeight = viewHeight;
            this.viewWidth = viewWidth;
            this.Height = this.heightConstant * viewHeight;
            this.Width = this.widthConstant * (viewWidth + 3);
        }

        private void initKeyLabel()
        {
            keysIndicator = new Label();
            keysIndicator.Text = "Keys";
            keysIndicator.Font = new Font(FontFamily.GenericMonospace, (this.heightConstant * 0.75f));
            keysIndicator.Height = this.heightConstant;
            Controls.Add(keysIndicator);
            NumberOfKeys = new Label();
            NumberOfKeys.Text = "" + this.controller.GetNumberOfKeys();
            NumberOfKeys.Font = new Font(FontFamily.GenericMonospace, (this.heightConstant * 0.75f));
            //keysIndicator.Location = new System.Drawing.Point(this.controller.GetColumns() * this.widthConstant, 10);
            //NumberOfKeys.Location = new System.Drawing.Point(this.controller.GetColumns() * this.widthConstant + this.heightConstant*7/8, 10 + this.heightConstant);
            keysIndicator.Location = new System.Drawing.Point(this.viewWidth * this.widthConstant, 10);
            NumberOfKeys.Location = new System.Drawing.Point(this.viewWidth * this.widthConstant + this.heightConstant * 7 / 8, 10 + this.heightConstant);
            Controls.Add(NumberOfKeys);
        }

        private void initCommands()
        {
            allKeyCommands = new List<KeyCommand>();
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
            this.lastDirection = DirectionFactory.CreateDirection(0, 0);
        }

        public void escapeCommand_executed(object sender, EventArgs e)
        {
            this.controller.Save();
            Application.Exit();
        }

        public void jCommand_executed(object sender, EventArgs e)
        {
            this.controller.InteractWithSpace(this.controller.GetPlayerDirection().GetDirectionEnum());
            this.update();
        }

        public void hCommand_executed(object sender, EventArgs e)
        {
            this.showHighlighted = !this.showHighlighted;
            this.update();
        }

        public void update()
        {
            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //base.OnPaint(e);
            drawBoard(e.Graphics);
            this.NumberOfKeys.Text = "" + this.controller.GetNumberOfKeys();
        }

        private void drawBoard(Graphics g)
        {
            //for (int i = 0; i < this.controller.GetRows(); i++)
            //{
            //    for (int j = 0; j < this.controller.GetColumns(); j++)
            //    {
            //        g.DrawImage(this.controller.GetSpace(i,j).Texture, j * this.widthConstant, i * this.heightConstant);
            //    }
            //}
            Point playerPosition = this.controller.GetPlayerPosition();
            for (int i = 0; i < this.viewHeight; i++)
            {
                for (int j = 0; j < this.viewWidth; j++)
                {
                    int row = playerPosition.X + (i - this.viewHeight / 2);
                    int col = playerPosition.Y + (j - this.viewWidth / 2);
                    bool validRow = 0 <= row && row < this.controller.GetRows();
                    bool validCol = 0 <= col && col < this.controller.GetColumns();
                    if (validCol && validRow)
                    {
                        g.DrawImage(this.controller.GetSpace(row, col).Texture, j * this.widthConstant, i * this.heightConstant);
                    }
                }
            }
            //g.DrawImage(SpaceFactory.CreateImage(SpaceType.Empty), playerPosition.Y * this.widthConstant, playerPosition.X * this.heightConstant);
            //g.DrawImage(SpaceFactory.CreatePlayerImage(this.lastDirection), playerPosition.Y * this.widthConstant, playerPosition.X * this.heightConstant);
            Point playerTilePosition = new Point(this.viewWidth/2, this.viewHeight/2);
            Point playerScreenPosition = new Point(playerTilePosition.X * this.widthConstant,playerTilePosition.Y * this.heightConstant);
            g.DrawImage(SpaceFactory.CreateImage(SpaceType.Empty), playerScreenPosition);
            g.DrawImage(SpaceFactory.CreatePlayerImage(this.lastDirection), playerScreenPosition);
            if (this.showHighlighted)
            {
//                Point selectedPoint = this.lastDirection.InteractionSpace(playerPosition);
                Point selectedPoint = this.lastDirection.InteractionSpace(playerTilePosition);
                g.DrawImage(SpaceFactory.CreateSelectedImage(), selectedPoint.Y * this.widthConstant, selectedPoint.X * this.heightConstant);
            }
        }

        public void KeyDownEvent(object sender, KeyEventArgs e)
        {
            foreach (KeyCommand command in allKeyCommands)
            {
                if (e.KeyData == command.CommandKey)
                {
                    command.ExecuteCommand();
                    if (command.CommandDirection != Direction.None)
                    {
                        this.lastDirection = DirectionFactory.CreateDirection(command.CommandDirection);
                    }
                    this.controller.MovePlayer(command.CommandDirection);
                    this.update();
                }
            }
        }

        public void PlayerWonEvent(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
