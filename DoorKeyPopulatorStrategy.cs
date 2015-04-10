using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KrazeForms
{
    public class DoorKeyPopulatorStrategy:ITreeTraversalStrategy
    {
        private static int backTrackAmount = 5;
        private Random rand;

        public DoorKeyPopulatorStrategy()
        {
            rand = new Random();
        }
        
        public void Traverse<T>(Tree<T> tree, Action<T> visitor)
        {
            Traverse(tree, visitor, 0);
        }

        private void Traverse<T>(Tree<T> tree, Action<T> visitor, int depth)
        {
            IList<Tree<T>> children = tree.GetChildren();
            int max = children.Count;
            int subTreePath = rand.Next(max);
            if (children.Count == 0)
            {
                if (depth > 4)
                {
                    Tree<T> parent = tree.Parent;
                    visitor(tree.Data);
                    //visitor(parent.Data);
                    //if (parent.Parent != null)
                    //{
                    //    parent.Parent.Marked = true;
                    //}
                    //parent.Marked = true;
                }
                //if (parent != null)
                //{
                //    visitor(tree.Data);
                //    //visitor(parent.Data);
                //    parent.Marked = true;
                //}
                //int backTrack = rand.Next(Math.Min(depth - 1,backTrackAmount));
                //int iter = 0;
                //while (iter < depth)
                //{
                //    if (parent.Parent != null)
                //    {
                //        parent = parent.Parent;
                //    }
                //    if (iter == backTrack)
                //    {
                //        visitor(parent.Data);
                //    }
                //    ++iter;
                //}
            }
            else 
            {
                Tree<T> parent = tree.Parent;
                if ((tree.Marked || children[subTreePath].Marked) && parent != null /*&& depth > 4*/)
                {
                    visitor(tree.Data);
                    //visitor(parent.Data);
                    //if (parent.Parent != null)
                    //{
                    //    parent.Parent.Marked = true;
                    //}
                    //parent.Marked = true;
                }
                else
                {
                    Traverse(children[subTreePath], visitor, depth + 1);
                }
            }
        }
    }
}
