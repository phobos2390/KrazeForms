using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace KrazeForms
{
    class KeySpace : ISpace
    {
        private SpaceType emptyType;
        private SpaceType hasKeyType;
        private bool hasKey;

        public KeySpace()
        {
            this.emptyType = SpaceType.Empty;
            this.hasKeyType = SpaceType.Key;
            this.hasKey = true;
        }

        public Image Texture
        {
            get
            {
                if (hasKey)
                {
                    return SpaceFactory.CreateImage(this.hasKeyType);
                }
                else
                {
                    return SpaceFactory.CreateImage(this.emptyType);
                }
            }
        }

        public bool PlayerCanMoveIntoSpace(Player thePlayer)
        {
            return true;
        }

        public void InteractWithPlayer(Player thePlayer)
        {
            if (hasKey)
            {
                thePlayer.NumberOfKeys++;
                hasKey = false;
            }
        }

        public char FileOutput()
        {
            if (hasKey)
            {
                return 'K';
            }
            else
            {
                return ' ';
            }
        }
    }
}
