using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace KrazeForms
{
    class MapBuilder
    {
        private ISpace[,] spaces;
        private int height;
        private int width;
        private bool heightSet;
        private bool widthSet;
        private bool arrCreated;

        public MapBuilder SetHeight(int height)
        {
            this.height = height;
            this.heightSet = true;
            if (this.widthSet)
            {
                spaces = new ISpace[this.height, this.width];
                this.arrCreated = true;
            }
            return this;
        }

        public MapBuilder SetWidth(int width)
        {
            this.width = width;
            this.widthSet = true;
            if (this.heightSet)
            {
                spaces = new ISpace[this.height, this.width];
                this.arrCreated = true;
            }
            return this;
        }

        public MapBuilder CreateWall(int row, int col)
        {
            if (arrCreated)
            {
                spaces[row, col] = SpaceFactory.CreateWall();
            }
            return this;
        }

        public MapBuilder SetSpace(int row, int col, ISpace space)
        {
            if (arrCreated)
            {
                spaces[row, col] = space;
            }
            return this;
        }

        public MapBuilder SetSpaces(int row, int col, ISpace space, int rowNum, int colNum)
        {
            for (int i = 0; i < rowNum; i++)
            {
                for (int j = 0; j < colNum; j++)
                {
                    int currRow = row + i;
                    int currCol = col + j;
                    this.SetSpace(currRow,currCol,space);
                }
            }
            return this;
        }

        public MapBuilder CreateWalls(int row, int col, int rowNum, int colNum)
        {
            return this.SetSpaces(row,col,SpaceFactory.CreateWall(),rowNum,colNum);
        }

        public MapBuilder CreateKeySpace(int row, int col)
        {
            if (arrCreated)
            {
                spaces[row, col] = SpaceFactory.CreateKeySpace();
            }
            return this;
        }

        public MapBuilder CreateKeySpaces(int row, int col, int rowNum, int colNum)
        {
            return this.SetSpaces(row, col, SpaceFactory.CreateKeySpace(), rowNum, colNum);
        }

        public MapBuilder CreateWinSpace(int row, int col)
        {
            if (arrCreated)
            {
                spaces[row, col] = SpaceFactory.CreateWinSpace();
            }
            return this;
        }

        public MapBuilder CreateWinSpaces(int row, int col, int rowNum, int colNum)
        {
            return this.SetSpaces(row, col, SpaceFactory.CreateWinSpace(), rowNum, colNum);
        }

        public MapBuilder CreateDoor(int row, int col, int keysRequired)
        {
            if (arrCreated)
            {
                spaces[row, col] = SpaceFactory.CreateDoor(keysRequired);
            }
            return this;
        }

        public MapBuilder CreateWinSpaces(int row, int col, int numKeys, int rowNum, int colNum)
        {
            return this.SetSpaces(row, col, SpaceFactory.CreateDoor(numKeys), rowNum, colNum);
        }

        public MapBuilder CreateBaseMapPattern(int row, int col, int rowNum, int colNum)
        {
            bool wallCol = true;
            bool wallRow = true;
            for (int i = 0; i < rowNum; i++)
            {
                for (int j = 0; j < colNum; j++)
                {
                    int currRow = row + i;
                    int currCol = col + j;
                    if (wallRow && wallCol)
                    {
                        this.CreateWall(currRow, currCol);
                    }
                    wallCol = !wallCol;
                }
                wallRow = !wallRow;
            }
            return this;
        }

        public MapBuilder CreateSpaceFromSpaceAndDirection(int row, int col, Direction direction, ISpace space)
        {
            if (this.arrCreated)
            {
                IDirection dir = DirectionFactory.CreateDirection(direction);
                Point except = dir.InteractionSpace(new Point(col, row));
                spaces[except.X, except.Y] = space;
            }
            return this;
        }

        public MapBuilder SetWallsAroundSpaceExceptAtDirection(int row, int col, Direction direction)
        {
            return SetWallsAroundSpaceExceptAtTwoDirections(row, col, direction, direction);
        }

        public MapBuilder SetWallsAroundSpaceExceptAtTwoDirections(int row, int col, Direction d1, Direction d2)
        {
            if (this.arrCreated)
            {
                this.CreateBaseMapPattern(row - 1, col - 1, 3, 3);
                foreach (Direction dirVal in Enum.GetValues(d1.GetType()))
                {
                    if (dirVal != d1 && dirVal != d2)
                    {
                        IDirection dir = DirectionFactory.CreateDirection(dirVal);
                        Point place = dir.InteractionSpace(new Point(col, row));
                        this.CreateSpaceFromSpaceAndDirection(place.X, place.Y, dirVal, SpaceFactory.CreateWall());
                    }
                }
            }
            return this;
        }

        public MapBuilder SetWallsAroundRectangle(int row, int col, int rowNum, int colNum)
        {
            if (this.arrCreated)
            {
                this.CreateWalls(row, col, rowNum, 1);
                this.CreateWalls(row, col, 1, colNum);
                this.CreateWalls(row, col + colNum - 1, rowNum, 1);
                this.CreateWalls(row + rowNum - 1, col, 1, colNum);
            }
            return this;
        }

        public Map CreateMap()
        {
            if (this.arrCreated)
            {
                return new Map(spaces);
            }
            else
            {
                return null;
            }
        }
    }
}
