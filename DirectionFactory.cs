using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KrazeForms
{
    public static class DirectionFactory
    {
        private static IList<IDirection> directions = initDirections();

        private static IList<IDirection> initDirections()
        {
            IList<IDirection> returnList = new List<IDirection>();
            returnList.Add(new Up());
            returnList.Add(new Down());
            returnList.Add(new Left());
            returnList.Add(new Right());
            returnList.Add(new NoDirection());
            return returnList;
        }

        public static IDirection CreateDirection(int deltaX, int deltaY)
        {
            IEnumerable<IDirection> direction = from dir in directions
                                                where dir.MovesInThatDirection(deltaX, deltaY)
                                                select dir;
            return direction.First();
        }

        public static IDirection CreateDirection(Direction direction)
        {
            IEnumerable<IDirection> retDir = from dir in directions
                                                where dir.CorrespondsToDirectionEnum(direction)
                                                select dir;
            return retDir.First();
        }
    }
}
