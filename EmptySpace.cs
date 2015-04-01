using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace KrazeForms
{
    class EmptySpace : ISpace
    {
        private SpaceType emptyType;

        public EmptySpace()
        {
            this.emptyType = SpaceType.Empty;
        }

        public Image Texture
        {
            get
            {
                return SpaceFactory.CreateImage(this.emptyType);
            }
        }

        public bool PlayerCanMoveIntoSpace(Player thePlayer)
        {
            return true;
        }

        public void InteractWithPlayer(Player thePlayer) { }

        public char FileOutput()
        {
            return ' ';
        }
    }
}
