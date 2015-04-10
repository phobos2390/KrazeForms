using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KrazeForms
{
    public interface IGraphTraversalStrategy
    {
        void Traverse<T>(Graph<T> graph, Action<T> visitor);
    }
}
