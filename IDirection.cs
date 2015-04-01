using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace KrazeForms
{
    public interface IDirection
    {
        int DeltaX
        {
            get;
        }
        int DeltaY
        {
            get;
        }
        bool MovesInThatDirection(int deltaX, int deltaY);
        bool CorrespondsToDirectionEnum(Direction direction);
        Point InteractionSpace(Point current);
    }
}
