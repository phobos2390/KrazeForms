using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KrazeForms
{
    class PreorderTraversalStrategy:ITreeTraversalStrategy
    {
        public void Traverse<T>(Tree<T> tree, Action<T> visitor)
        {
            visitor(tree.Data);
            foreach(Tree<T> subtree in tree.GetChildren())
            {
                Traverse<T>(subtree, visitor);
            }
        }
    }
}
