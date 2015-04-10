using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace KrazeForms
{
    class Model
    {
        private Map map;
        private Player player;

        public Model(Map map, Player player)
        {
            this.map = map;
            this.player = player;
        }

        public Model(String mapFileName, String playerFileName)
        {
            this.Load(playerFileName, mapFileName);
        }

        public bool CanMovePlayer(Direction moveDirection)
        {
            IDirection dir = DirectionFactory.CreateDirection(moveDirection);
            Point newPoint = dir.InteractionSpace(player.PlayerPosition);
            if (newPoint.X >= this.map.Rows 
                || newPoint.Y >= this.map.Columns
                ||newPoint.X < 0 
                || newPoint.Y < 0)
            {
                return false;
            }
            else
            {
                ISpace toMoveTo = this.map[newPoint.X, newPoint.Y];
                return toMoveTo.PlayerCanMoveIntoSpace(player);
            }
        }

        public void MovePlayer(Direction moveDirection)
        {
            IDirection dir = DirectionFactory.CreateDirection(moveDirection);
            Point newPoint = dir.InteractionSpace(player.PlayerPosition);
            ISpace toMoveTo = this.map[newPoint.X, newPoint.Y];
            toMoveTo.InteractWithPlayer(player);
            if (moveDirection != Direction.None)
            {
                player.CurrentDirection = dir;
            }
            player.PlayerPosition = newPoint;
        }

        public void InteractWithSpace(Direction interactionDirection)
        {
            IDirection dir = DirectionFactory.CreateDirection(interactionDirection);
            Point newPoint = dir.InteractionSpace(player.PlayerPosition);
            ISpace toMoveTo = this.map[newPoint.X, newPoint.Y];
            toMoveTo.InteractWithPlayer(player);
        }

        public IDirection GetPlayerDirection()
        {
            return this.player.CurrentDirection;
        }

        public int GetNumberOfKeys()
        {
            return this.player.NumberOfKeys;
        }

        public Point GetPlayerPosition()
        {
            return this.player.PlayerPosition;
        }

        public Point GetSelectedPoint()
        {
            return this.player.CurrentDirection.InteractionSpace(this.player.PlayerPosition);
        }

        public ISpace GetSpace(int x, int y)
        {
            return this.map[x, y];
        }

        public int GetRows()
        {
            return this.map.Rows;
        }

        public int GetColumns()
        {
            return this.map.Columns;
        }

        public bool Won
        {
            get
            {
                return player.HasWon;
            }
        }

        public void Save(string playerFileName,string mazeFileName)
        {
            System.IO.StreamWriter playerWriter = new System.IO.StreamWriter(playerFileName);
            playerWriter.WriteLine(player.PlayerPosition.X);
            playerWriter.WriteLine(player.PlayerPosition.Y);
            playerWriter.WriteLine(player.NumberOfKeys);
            playerWriter.Close();
            System.IO.StreamWriter fileWriter = new System.IO.StreamWriter(mazeFileName);
            fileWriter.WriteLine(map.Rows);
            fileWriter.WriteLine(map.Columns);
            for (int i = 0; i < map.Rows; i++)
            {
                int num = 1;
                char prev = map[i, 0].FileOutput();
                for (int j = 1; j < map.Columns; j++)
                {
                    char curr = map[i, j].FileOutput();
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

        public void Load(string playerFileName, string mazeFileName)
        {
            System.IO.StreamReader file = new System.IO.StreamReader(playerFileName);
            this.player = new Player(file);
            file.Close();
            System.IO.StreamReader fileReader = new System.IO.StreamReader(mazeFileName);
            string buffer;
            int index = Int16.Parse(fileReader.ReadLine());
            int index2 = Int16.Parse(fileReader.ReadLine());
            MapBuilder builder = new MapBuilder();
            builder.SetHeight(index)
                   .SetWidth(index2);
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
                    builder.SetSpaces(i, colIter, SpaceFactory.CreateSpaceFromChar(buffer[j]), 1, num);
                    colIter += num;
                }
            }
            fileReader.Close();
            this.map = builder.CreateMap();
        }
    }
}
