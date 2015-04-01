using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace KrazeForms
{
    interface IImageFactory
    {
        Image CreateImage(SpaceType type, int num);

        Image CreateImage(SpaceType type);

        Image CreateSelectedImage();

        Image CreatePlayerImage(IDirection dir);

        int GetSpaceHeightConstant();

        int GetSpaceWidthConstant();
    }
}
