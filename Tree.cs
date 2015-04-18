using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KrazeForms
{
    public class Tree<T>
    {
        private T data;
        private ITreeTraversalStrategy traverser;
        private Tree<T> parent;
        private IList<Tree<T>> subTree;
        private bool marked;

        public Tree(T data)
        {
            this.data = data;
            this.parent = null;
            this.marked = false;
            this.subTree = new List<Tree<T>>();
        }

        public void SetTraverser(ITreeTraversalStrategy traverser)
        {
            this.traverser = traverser;
        }

        public bool Marked
        {
            get
            {
                return this.marked;
            }
            set
            {
                this.marked = value;
            }
        }

        public Tree<T> Parent
        {
            get
            {
                return this.parent;
            }
        }

        public T Data
        {
            get
            {
                return this.data;
            }
            set
            {
                this.data = value;
            }
        }

        public Tree<T> GetChild(int index)
        {
            return this.subTree[index];
        }

        public IList<Tree<T>> GetChildren()
        {
            return this.subTree;
        }

        public void AddChild(Tree<T> child)
        {
            this.subTree.Add(child);
            child.parent = this;
        }

        public void Traverse(Action<T> visitor)
        {
            this.traverser.Traverse<T>(this,visitor);
        }
    }
}
