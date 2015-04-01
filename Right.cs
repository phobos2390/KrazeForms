using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KrazeForms
{
    class Right : AbstractDirection
    {
        public Right() : base(1, 0) { }

        public override bool CorrespondsToDirectionEnum(Direction direction)
        {
            return direction == Direction.Right;
        }
    }
}
