using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace KrazeForms
{
    interface ISpace
    {
        Image Texture
        {
            get;
        }

        bool PlayerCanMoveIntoSpace(Player thePlayer);

        void InteractWithPlayer(Player thePlayer);

        char FileOutput();
    }
}
