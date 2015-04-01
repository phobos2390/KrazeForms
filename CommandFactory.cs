using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KrazeForms
{
    public static class CommandFactory
    {
        public static KeyCommand Create(Keys key, Direction direction)
        {
            return new KeyCommand(key, DirectionFactory.CreateDirection(direction));
        }

        public static KeyCommand Create(Keys key)
        {
            return new KeyCommand(key);
        }
    }
}
