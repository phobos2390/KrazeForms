using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KrazeForms
{
    class Left : AbstractDirection
    {
        public Left() : base(-1, 0) { }

        public override bool CorrespondsToDirectionEnum(Direction direction)
        {
            return direction == Direction.Left;
        }
    }
}
