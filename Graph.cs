using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KrazeForms
{
    public class Graph<T>
    {
        private IDictionary<T, IList<T>> graph;
        private IGraphTraversalStrategy traverser;

        public Graph()
        {
            this.graph = new Dictionary<T,IList<T>>();
        }

        public IList<T> this[T data]
        {
            get
            {
                return graph[data];
            }
        }

        public ICollection<T> GetNodeList()
        {
            return graph.Keys;
        }

        public void Add(T data)
        {
            IList<T> connections = new List<T>();
            this.graph.Add(data, connections);
        }

        public void SetAdjacent(T source, T destination)
        {
            this.graph[source].Add(destination);
        }

        public void SetTraverser(IGraphTraversalStrategy traverser)
        {
            this.traverser = traverser;
        }

        public void Traverse(Action<T> visitor)
        {
            this.traverser.Traverse<T>(this, visitor);
        }
    }
}
