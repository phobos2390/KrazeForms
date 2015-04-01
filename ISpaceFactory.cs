using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KrazeForms
{
    interface ISpaceFactory
    {
        ISpace CreateSpaceFromChar(char type);
        ISpace CreateWall();
        ISpace CreateKeySpace();
        ISpace CreateEmptySpace();
        ISpace CreateWinSpace();
        ISpace CreateDoor(int keys);
    }
}
