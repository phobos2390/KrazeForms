using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KrazeForms
{
    public class KeyCommand
    {
        private Keys commandKey;
        private IDirection direction;

        public event EventHandler executed;

        public KeyCommand(Keys commandKey, IDirection direction)
        {
            this.commandKey = commandKey;
            this.direction = direction;
        }

        public KeyCommand(Keys commandKey)
        {
            this.commandKey = commandKey;
            this.direction = new NoDirection();
        }

        public int DeltaX
        {
            get
            {
                return this.direction.DeltaX;
            }
        }

        public int DeltaY
        {
            get
            {
                return this.direction.DeltaY;
            }
        }

        public Keys CommandKey
        {
            get
            {
                return commandKey;
            }
        }

        public Direction CommandDirection
        {
            get
            {
                return this.direction.GetDirectionEnum();
            }
        }

        public void ExecuteCommand()
        {
            if (executed != null)
            {
                executed(this, new EventArgs());
            }
        }
    }
}
