using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KrazeForms
{
    public interface ITreeTraversalStrategy
    {
        void Traverse<T>(Tree<T> tree, Action<T> visitor);
    }
}
