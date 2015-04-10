using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace KrazeForms
{
    public class MazeGraphBuilder
    {
        private Point[,] positions;
        private Graph<Point> graph;
        private Tree<Point> tree;
        private Tree<Point> currentIter;
        private int height;
        private int width;
        private int graphHeight;
        private int graphWidth;
        private IList<Point> edges;

        public MazeGraphBuilder SetSize(int height, int width)
        {
            this.height = height;
            this.width = width;
            this.graphHeight = (height - 1) / 2;
            this.graphWidth = (width - 1) / 2;
            this.positions = new Point[graphHeight, graphWidth];
            for (int i = 0; i < graphHeight; i++)
            {
                for (int j = 0; j < graphWidth; j++)
                {
                    int row = i * 2 + 1;
                    int col = j * 2 + 1;
                    positions[i, j] = new Point(row,col);
                }
            }
            return this;
        }

        public Graph<Point> CreateGraph()
        {
            graph = new Graph<Point>();
            foreach (Point p in positions)
            {
                graph.Add(p);
            }
            this.edges = new List<Point>();
            for (int i = 0; i < this.positions.GetUpperBound(0) + 1; i++)
            {
                for (int j = 0; j < this.positions.GetUpperBound(1) + 1; j++)
                {
                    Point current = positions[i, j];
                    for (int k = 0; k < 2; k++)
                    {
                        int change = 2*k - 1;
                        int newRow = i + change;
                        int newCol = j + change;
                        bool rowInBounds = (0 <= (newRow) && (newRow) <= this.positions.GetUpperBound(0));
                        bool validPosition = rowInBounds;
                        bool edge = false;
                        if (validPosition)
                        {
                            graph.SetAdjacent(current, positions[newRow, j]);
                        }
                        else
                        {
                            edge = true;
                        }
                        bool colInBounds = (0 <= (newCol) && (newCol) <= this.positions.GetUpperBound(1));
                        validPosition = colInBounds;
                        if (validPosition)
                        {
                            graph.SetAdjacent(current, positions[i, newCol]);
                        }
                        else
                        {
                            edge = true;
                        }
                        if (edge)
                        {
                            this.edges.Add(current);
                        }
                    }
                }
            }
            return graph;
        }

        private Tree<Point> findData(Tree<Point> tree, Point data)
        {
            if (tree.Data == data)
            {
                return tree;
            }
            else
            {
                IList<Tree<Point>> children = tree.GetChildren();
                foreach (Tree<Point> subtree in children)
                {
                    if (subtree.Data == data)
                    {
                        return subtree;
                    }
                    else 
                    {
                        Tree<Point> foundData = findData(subtree, data);
                        if (foundData != null)
                        {
                            return foundData;
                        }
                    }
                }
                return null;
            }
        }

        private ITreeTraversalState currState;

        private interface ITreeTraversalState
        {
            ITreeTraversalState NextState { set; }
            void Read(Point data);
        }

        private class FirstState : ITreeTraversalState
        {
            private MazeGraphBuilder builder;
            private ITreeTraversalState secondState;

            public FirstState(MazeGraphBuilder builder, ITreeTraversalState secondState)
            {
                this.builder = builder;
                this.secondState = secondState;
            }

            public ITreeTraversalState NextState
            {
                set
                {
                    this.secondState = value;
                }
            }

            public void Read(Point data)
            {
                this.builder.tree.Data = data;
                this.builder.currentIter = this.builder.tree;
                this.builder.currState = secondState;
            }
        }

        private class SecondState:ITreeTraversalState
        {
            private MazeGraphBuilder builder;
            private ITreeTraversalState thirdState;

            public SecondState(MazeGraphBuilder builder, ITreeTraversalState thirdState)
            {
                this.builder = builder;
                this.thirdState = thirdState;
            }

            public ITreeTraversalState NextState
            {
                set
                {
                    this.thirdState = value;
                }
            }

            public void Read(Point data)
            {
                this.builder.currentIter = this.builder.findData(this.builder.tree, data);
                this.builder.currState = thirdState;
            }
        }

        private class ThirdState : ITreeTraversalState
        {
            private MazeGraphBuilder builder;
            private ITreeTraversalState firstState;

            public ThirdState(MazeGraphBuilder builder, ITreeTraversalState firstState)
            {
                this.builder = builder;
                this.firstState = firstState;
            }

            public ITreeTraversalState NextState
            {
                set
                {
                    this.firstState = value;
                }
            }

            public void Read(Point data)
            {
                this.builder.currentIter.AddChild(new Tree<Point>(data));
                this.builder.currState = firstState;
            }
        }

        public void graphTraversalAction(Point data)
        {
            currState.Read(data);
        }

        public Tree<Point> CreateRandomSpanningTree()
        {
            this.graph.SetTraverser(new RandomSpanningTreeTraverser());
            ITreeTraversalState thirdState = new ThirdState(this, null);
            ITreeTraversalState secondState = new SecondState(this, thirdState);
            ITreeTraversalState firstState = new FirstState(this, thirdState);
            thirdState.NextState = secondState;
            this.currState = firstState;
            this.tree = new Tree<Point>(Point.Empty);
            this.graph.Traverse(graphTraversalAction);
            return this.tree;
        }

        private MapBuilder mapBuilder;

        private IList<ISpace> doorsAndKeys;
        
        private void initDoorsAndKeys()
        {
            doorsAndKeys = new List<ISpace>();
            //doorsAndKeys.Add(SpaceFactory.CreateWinSpace());
            doorsAndKeys.Add(SpaceFactory.CreateDoor(9));
            doorsAndKeys.Add(SpaceFactory.CreateKeySpace());
            doorsAndKeys.Add(SpaceFactory.CreateDoor(8));
            doorsAndKeys.Add(SpaceFactory.CreateKeySpace());
            doorsAndKeys.Add(SpaceFactory.CreateDoor(7));
            doorsAndKeys.Add(SpaceFactory.CreateKeySpace());
            doorsAndKeys.Add(SpaceFactory.CreateDoor(6));
            doorsAndKeys.Add(SpaceFactory.CreateKeySpace());
            doorsAndKeys.Add(SpaceFactory.CreateDoor(5));
            doorsAndKeys.Add(SpaceFactory.CreateKeySpace());
            doorsAndKeys.Add(SpaceFactory.CreateDoor(4));
            doorsAndKeys.Add(SpaceFactory.CreateKeySpace());
            doorsAndKeys.Add(SpaceFactory.CreateDoor(3));
            doorsAndKeys.Add(SpaceFactory.CreateKeySpace());
            doorsAndKeys.Add(SpaceFactory.CreateDoor(2));
            doorsAndKeys.Add(SpaceFactory.CreateKeySpace());
            doorsAndKeys.Add(SpaceFactory.CreateDoor(1));
            doorsAndKeys.Add(SpaceFactory.CreateKeySpace());
            doorsAndKeys.Add(SpaceFactory.CreateEmptySpace());
        }

        public void initialTraversal(Point data)
        {
            char fileOutput = this.doorsAndKeys.First().FileOutput();
            //if (fileOutput == 'A')
            //{
            //    if (this.first && this.edges.Contains(data))
            //    {
            //        Direction dir = Direction.None;
            //        if (data.X == 1)
            //        {
            //            dir = Direction.Up;
            //        }
            //        else if (data.Y == 1)
            //        {
            //            dir = Direction.Left;
            //        }
            //        else if (data.X + 2 == this.height)
            //        {
            //            dir = Direction.Down;
            //        }
            //        else
            //        {
            //            dir = Direction.Right;
            //        }
            //        IDirection direction = DirectionFactory.CreateDirection(dir);
            //        mapBuilder.CreateWinSpace(data.X + direction.DeltaY, data.Y + direction.DeltaX);
            //        this.doorsAndKeys.RemoveAt(0);
            //    }
            //}
            //else 
            //if(!this.first && '0' <= fileOutput && fileOutput <= '9')
            //{
            //    Direction dir = mapBuilder.DirectionOpenAroundSpace(data.X, data.Y);
            //    IDirection direction = DirectionFactory.CreateDirection(dir);
            //    Point newPoint = direction.InteractionSpace(data);
            //    mapBuilder.SetSpace(newPoint.X, newPoint.Y, SpaceFactory.CreateSpaceFromChar(fileOutput));
            //    this.doorsAndKeys.RemoveAt(0);
            //}
            //else 
            if (/*this.first && */fileOutput == 'K' && !this.mapBuilder.SpaceOccupied(data.X,data.Y))
            {
                Tree<Point> nodeWithData = this.findData(this.tree, data);
                Tree<Point> parent = nodeWithData.Parent;
                int size = this.doorsAndKeys.Count/2;
                Tree<Point> current = nodeWithData;
                while (current != null && size > 0)
                {
                    current = current.Parent;
                    --size;
                }
                if (size == 0)
                {
                    mapBuilder.SetSpace(data.X, data.Y, SpaceFactory.CreateKeySpace());
                    this.doorsAndKeys.RemoveAt(0);
                    if (this.doorsAndKeys.First().FileOutput() != ' ')
                    {
                        Tree<Point> currentParent = parent;
                        Tree<Point> currentChild = nodeWithData;
                        while (currentParent.Parent != null && currentParent.GetChildren().Count < 2)
                        {
                            currentChild = currentParent;
                            currentParent = currentParent.Parent;
                        }
                        //currentParent.Marked = true;
                        Point parentSpace = currentParent.Data;
                        Point childSpace = currentChild.Data;
                        Point midPoint = new Point((childSpace.X + parentSpace.X) / 2, (childSpace.Y + parentSpace.Y) / 2);
                        mapBuilder.SetSpace(midPoint.X, midPoint.Y, SpaceFactory.CreateSpaceFromChar(this.doorsAndKeys.First().FileOutput()));
                        this.doorsAndKeys.RemoveAt(0);
                    }
                }
            }
            //this.first = !this.first;
        }

        public Map CreateMapFromSpanningTree()
        {
            mapBuilder = new MapBuilder();
            MapBuilder builder = mapBuilder;// new MapBuilder();
            builder.SetHeight(this.height)
                .SetWidth(this.width)
                .CreateFilledMapPattern(0,0,this.height,this.width);
            Tree<Point> spanningTree = this.CreateRandomSpanningTree();
            //this.preOrderTraversal = new StringBuilder();
            //spanningTree.SetTraverser(new PreorderTraversalStrategy());
            //spanningTree.Traverse(this.traverseTree);
            builder = recCreateMapFromTree(spanningTree, builder);
            builder.CreateWinSpace(this.height - 2, this.width - 1);
            Point endSpace = new Point(this.height - 2, this.width - 2);
            Tree<Point> parent = this.findData(spanningTree, endSpace).Parent;
            parent.Marked = true;
            Point maxDoorSpace = parent.Data;
            Point midPoint = new Point((endSpace.X + maxDoorSpace.X) / 2, (endSpace.Y + maxDoorSpace.Y) / 2);
            this.initDoorsAndKeys();
            ISpace firstSpace = this.doorsAndKeys[0];
            this.doorsAndKeys.RemoveAt(0);
            builder.SetSpace(midPoint.X,midPoint.Y,firstSpace);
            ITreeTraversalStrategy populator = new DoorKeyPopulatorStrategy();
            spanningTree.SetTraverser(populator);
            while (this.doorsAndKeys.First().FileOutput() != ' ')
            {
                spanningTree.Traverse(initialTraversal);
            }
            return builder.CreateMap();
        }

        private MapBuilder recCreateMapFromTree(Tree<Point> currIter, MapBuilder recBuilder)
        {
            MapBuilder builder = recBuilder;
            //builder.SetWallsAroundRectangle(currIter.Data.Y - 1, currIter.Data.X - 1, 3, 3);
            foreach (Tree<Point> subTree in currIter.GetChildren())
            {
                //int minRow = Math.Min(currIter.Data.X, subTree.Data.X);
                //int maxRow = Math.Max(currIter.Data.X, subTree.Data.X);
                //int minCol = Math.Min(currIter.Data.Y, subTree.Data.Y);
                //int maxCol = Math.Max(currIter.Data.Y, subTree.Data.Y);
                //int numCol = maxCol - minCol;
                //int numRow = maxRow - minRow;
                //builder.SetSpaces
                //    (minRow,
                //    minCol,
                //    SpaceFactory.CreateEmptySpace(),
                //    numRow + 1,
                //    numCol + 1);
                builder.CreateEmptySpace((currIter.Data.X + subTree.Data.X) / 2, (currIter.Data.Y + subTree.Data.Y) / 2);
                //int dx = subTree.Data.X - currIter.Data.X;
                //int dy = subTree.Data.Y - currIter.Data.Y;
                //IDirection dir = DirectionFactory.CreateDirection(dy/2, dy/2);
                //builder.CreateEmptySpace(currIter.Data.X, currIter.Data.Y);
                //builder.CreateSpaceFromSpaceAndDirection
                //   (currIter.Data.X,
                //    currIter.Data.Y, 
                //    dir.GetDirectionEnum(), 
                //    SpaceFactory.CreateEmptySpace());
                builder = this.recCreateMapFromTree(subTree, builder);
            }
            return builder;
        }
    }
}
