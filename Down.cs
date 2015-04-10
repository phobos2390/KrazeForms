using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KrazeForms
{
    class Down : AbstractDirection
    {
        public Down() : base(0, 1) { }

        public override bool CorrespondsToDirectionEnum(Direction direction)
        {
            return direction == Direction.Down;
        }

        public override Direction GetDirectionEnum()
        {
            return Direction.Down;
        }
    }
}
