using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace KrazeForms
{
    class WinSpace : ISpace
    {
        public Image Texture
        {
            get
            {
                return SpaceFactory.CreateImage(SpaceType.Win);
            }
        }

        public bool PlayerCanMoveIntoSpace(Player thePlayer)
        {
            return true;
        }

        public void InteractWithPlayer(Player thePlayer)
        {
            thePlayer.HasWon = true;
        }

        public char FileOutput()
        {
            return 'A';
        }
    }
}
