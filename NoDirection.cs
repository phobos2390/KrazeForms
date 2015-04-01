using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KrazeForms
{
    public class NoDirection : AbstractDirection
    {
        public NoDirection() : base(0, 0) { }

        public override bool CorrespondsToDirectionEnum(Direction direction)
        {
            return false;
        }
    }
}
