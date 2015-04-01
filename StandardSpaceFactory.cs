using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KrazeForms
{
    class StandardSpaceFactory:ISpaceFactory
    {
        public ISpace CreateSpaceFromChar(char type)
        {
            if (type == 'W')
            {
                return this.CreateWall();
            }
            else if (type == 'K')
            {
                return this.CreateKeySpace();
            }
            else if (type == ' ')
            {
                return this.CreateEmptySpace();
            }
            else if (type == 'A')
            {
                return this.CreateWinSpace();
            }
            else if ('1' <= type && type <= '9')
            {
                int theIndex = (int)((int)type - (int)'0');
                return this.CreateDoor(theIndex);
            }
            else
            {
                return null;
            }
        }

        public ISpace CreateWall()
        {
            return new WallSpace();
        }

        public ISpace CreateKeySpace()
        {
            return new KeySpace();
        }

        public ISpace CreateEmptySpace()
        {
            return new EmptySpace();
        }

        public ISpace CreateWinSpace()
        {
            return new WinSpace();
        }

        public ISpace CreateDoor(int keys)
        {
            return new DoorSpace(keys);
        }
    }
}
