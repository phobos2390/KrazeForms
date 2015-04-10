using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KrazeForms
{
    public class Map
    {
        private ISpace[,] spaces;

        public Map(ISpace[,] spaces)
        {
            this.spaces = spaces;
        }

        public ISpace this[int index1, int index2]
        {
            get
            {
                int returnIndex1;
                int returnIndex2;

                if (spaces.GetUpperBound(0) < index1)
                {
                    returnIndex1 = spaces.GetUpperBound(0);
                }
                else if (0 > index1)
                {
                    returnIndex1 = 0;
                }
                else
                {
                    returnIndex1 = index1;
                }
                if (spaces.GetUpperBound(1) < index2)
                {
                    returnIndex2 = spaces.GetUpperBound(1);
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

        public int Rows
        {
            get
            {
                return this.spaces.GetUpperBound(0) + 1;
            }
        }

        public int Columns
        {
            get
            {
                return this.spaces.GetUpperBound(1) + 1;
            }
        }

        public int GetUpperBound(int dim)
        {
            return this.spaces.GetUpperBound(dim);
        }
    }
}
