using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KrazeForms
{
    class MoveEventArgs : EventArgs
    {
        private int moveX;
        private int moveY;

        public MoveEventArgs(int moveX, int moveY)
        {
            this.moveX = moveX;
            this.moveY = moveY;
        }

        public int MoveX
        {
            get
            {
                return moveX;
            }
        }

        public int MoveY
        {
            get
            {
                return moveY;
            }
        }
    }
}
