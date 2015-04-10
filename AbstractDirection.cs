using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace KrazeForms
{
    public abstract class AbstractDirection:IDirection
    {
        private int dX;
        private int dY;

        protected AbstractDirection(int deltaX, int deltaY)
        {
            this.dX = deltaX;
            this.dY = deltaY;
        }

        public int DeltaX
        {
            get { return dX; }
        }

        public int DeltaY
        {
            get { return dY; }
        }

        public bool MovesInThatDirection(int deltaX, int deltaY)
        {
            return dX == deltaX && dY == deltaY;
        }

        public abstract bool CorrespondsToDirectionEnum(Direction direction);

        public abstract Direction GetDirectionEnum();

        public Point InteractionSpace(Point current)
        {
            Point returnCurrent = new Point(current.X + dY,current.Y + dX);
            return returnCurrent;
        }

        public override bool Equals(object obj)
        {
            if (obj is IDirection)
            {
                IDirection dir = (IDirection)obj;
                return this.DeltaX == dir.DeltaX && this.DeltaY == dir.DeltaY;
            }
            else
            {
                return false;
            }
        }

        public static bool operator ==(AbstractDirection d1, AbstractDirection d2)
        {
            if (object.Equals(d1, null))
            {
                return object.Equals(d2, null);
            }
            else if(object.Equals(d2, null))
            {
                return false;
            }
            else
            {
                return d1.DeltaX == d2.DeltaY && d1.DeltaY == d2.DeltaY;
            }
        }

        public static bool operator !=(AbstractDirection d1, AbstractDirection d2)
        {
            return !(d1 == d2);
        }

        public override int GetHashCode()
        {
            return 7*this.dX + 71*this.dY;
        }

        public override string ToString()
        {
            return "Direction delta's <" + dX + "," + dY + ">";
        }
    }
}
