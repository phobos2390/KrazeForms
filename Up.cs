using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KrazeForms
{
    class Up : AbstractDirection
    {
        public Up() : base(0, -1) { }

        public override bool CorrespondsToDirectionEnum(Direction direction)
        {
            return direction == Direction.Up;
        }

        public override Direction GetDirectionEnum()
        {
            return Direction.Up;
        }
    }
}
