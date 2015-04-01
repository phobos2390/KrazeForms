using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace KrazeForms
{
    class DoorSpace : ISpace
    {
        private int index;

        public DoorSpace(int index)
        {
            this.index = index;
        }

        public Image Texture
        {
            get
            {
                return SpaceFactory.CreateImage(SpaceType.Door, this.index);
            }
        }

        public bool PlayerCanMoveIntoSpace(Player thePlayer)
        {
            return thePlayer.NumberOfKeys >= this.index;
        }

        public void InteractWithPlayer(Player thePlayer) { }

        public char FileOutput()
        {
            return ("" + index)[0];
        }
    }
}
