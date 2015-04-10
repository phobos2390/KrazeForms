using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KrazeForms
{
    class RandomSpanningTreeTraverser:IGraphTraversalStrategy
    {
        private Random rand;

        public RandomSpanningTreeTraverser()
        {
            rand = new Random();
        }

        public void Traverse<T>(Graph<T> graph, Action<T> visitor)
        {
            T[] nodes = graph.GetNodeList().ToArray();
            bool[] marked = new bool[nodes.Length];
            T first = nodes[0];
            int currentIndex = 0;
            T current = first;
            while (marked.Contains(false))
            {
                visitor(current);
                marked[currentIndex] = true;
                IList<T> adjacentNeighbors = graph[current];
                IList<T> unmarkedNeighbors = new List<T>();
                IList<T> markedNeighbors = new List<T>();
                foreach (T node in adjacentNeighbors)
                {
                    int indexOfNode = Array.IndexOf(nodes, node);
                    if (!marked[indexOfNode])
                    {
                        unmarkedNeighbors.Add(node);
                    }
                    else
                    {
                        markedNeighbors.Add(node);
                    }
                }
                int possibleNeighbors = unmarkedNeighbors.Count;
                if (possibleNeighbors > 1)
                {
                    if (markedNeighbors.Count > 0)
                    {
//                        visitor(markedNeighbors[0]);
                        visitor(current);
                    } 
                    int neighborIndex = rand.Next(possibleNeighbors);
                    currentIndex = Array.IndexOf(nodes, unmarkedNeighbors[neighborIndex]);
                    current = nodes[currentIndex];
//                    visitor(current);
                }
                else if (possibleNeighbors > 0)
                {
                    visitor(current);
                    current = unmarkedNeighbors.First();
                    currentIndex = Array.IndexOf(nodes, current);
//                    visitor(markedNeighbors[0]);
//                    visitor(current);
                }
                else if (marked.Contains(false))
                {
                    currentIndex = Array.IndexOf(marked, false);
                    current = nodes[currentIndex];
                    adjacentNeighbors = graph[current];
                    unmarkedNeighbors = new List<T>();
                    markedNeighbors = new List<T>();
                    foreach (T node in adjacentNeighbors)
                    {
                        int indexOfNode = Array.IndexOf(nodes, node);
                        if (!marked[indexOfNode])
                        {
                            unmarkedNeighbors.Add(node);
                        }
                        else
                        {
                            markedNeighbors.Add(node);
                        }
                    }
                    visitor(markedNeighbors[0]);
//                    visitor(current);
                }
            }
        }
    }
}
