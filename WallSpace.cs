using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace KrazeForms
{
    class WallSpace : ISpace
    {
        public Image Texture
        {
            get
            {
                return SpaceFactory.CreateImage(SpaceType.Wall);
            }
        }

        public bool PlayerCanMoveIntoSpace(Player thePlayer)
        {
            return false;
        }

        public void InteractWithPlayer(Player thePlayer) { }

        public char FileOutput()
        {
            return 'W';
        }
    }
}
