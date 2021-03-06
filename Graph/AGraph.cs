﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph
{
    public abstract class AGraph<T> : IGraph<T> where T : IComparable<T>
    {
        #region Attributes
        //This is the main data structures that stores all of the vertices.
        protected List<Vertex<T>> vertices;
        //Use a dictionary(Hashtable, to store the index of each data element in the vertices array.
        protected Dictionary<T, int> revLookUp;

        protected int numEdges;
        protected bool isDirected;
        protected bool isWeighted;
        #endregion

        #region Constructor
        public AGraph()
        {
            //Set up the vertices array
            vertices = new List<Vertex<T>>();
            //Set up the reverse lookup dictionary (efficiency purposes)
            revLookUp = new Dictionary<T, int>();
            //Initialize number of edges
            numEdges = 0;
        }
        #endregion

        #region Properties
        //Make this overrideable so the Undirected Implementation can override it.
        public virtual int NumEdges
        {
            get
            {
                return numEdges;
            }
        }

        public int NumVertices
        {
            get
            {
                return vertices.Count;
            }
        }
        #endregion

        #region Abstract Methods

        //A helper method so we can code the other AddEdge methods within this class
        protected abstract void AddEdge(Edge<T> e);
        public abstract bool HasEdge(T from, T to);
        public abstract Edge<T> GetEdge(T from, T to);
        public abstract void RemoveEdge(T from, T to);
        public abstract IEnumerable<Vertex<T>> EnumerateNeighbours(T data);

        //Implements functionality in the child class that will make room for
        //Edges of a vertex added in this class.
        protected abstract void AddVertexAdjustEdges(Vertex<T> v);
        protected abstract void RemoveVertexAdjustEdges(Vertex<T> v);

        //Return an array of all edges in the graph.
        protected abstract Edge<T>[] GetAllEdges();
        #endregion

        public virtual void AddEdge(T from, T to)
        {
            //if this is the first edge, set isWeighted
            if (numEdges == 0)
            {
                isWeighted = false;
            }
            else if (isWeighted)
            {
                throw new ApplicationException("You crazy. You can't add an unweighted edge to a weighted graph.");
            }

            //Create an edge object
            Edge<T> e = new Edge<T>(GetVertex(from), GetVertex(to));
            //Add the edge to the child class
            AddEdge(e);
        }

        public virtual void AddEdge(T from, T to, double weight)
        {
            if (numEdges == 0)
            {
                isWeighted = true;
            }
            else if (!isWeighted)
            {
                throw new ApplicationException("You crazy. You can't add a weighted edge to an unweighted graph.");
            }

            Edge<T> e = new Edge<T>(GetVertex(from), GetVertex(to), weight);
            AddEdge(e);
        }

        //Can code the implementation here, but we need to force the child class
        //implementations to add space for future edges to this vertex.
        public void AddVertex(T data)
        {
            //Check if the vertex already exists
            if (HasVertex(data))
            {
                throw new ApplicationException("Vertex already exists");
            }
            //Create a new vertext object
            Vertex<T> v = new Vertex<T>(vertices.Count, data);
            //Add to the vertices list
            vertices.Add(v);
            //Also, add to the reverse look up Dictionary
            revLookUp.Add(data, v.Index);
            //Tell child to create room for edges
            AddVertexAdjustEdges(v);
        }

        public IEnumerable<Vertex<T>> EnumerateVertices()
        {
            //Since a List<> is IEnumberable just return the vertices.
            return vertices;
        }

        public Vertex<T> GetVertex(T data)
        {
            //If the item is not in the graph, thrown an exception
            if (!HasVertex(data))
            {
                throw new ApplicationException("No such vertex");
            }
            //Get the data's index into the vertices array
            //Note that the [] are overloaded in Microsoft's dictionary to call "Get()"
            int index = revLookUp[data];
            //Get the vertex
            return vertices[index];
        }

        public bool HasAnEdge(T from, T to)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Looks up the data in the dictionary to see if it exists.
        /// This is more efficient than looking it up in the vertices array.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool HasVertex(T data)
        {
            return revLookUp.ContainsKey(data);
        }

        public IGraph<T> MinimumSpanningTree()
        {
            //Each vertex is called a tree in the forest
            AGraph<T>[] Forest = new AGraph<T>[vertices.Count];
            //Get the sorted list of edges from the original graph
            Edge<T>[] eArray = GetAllEdges();
            //Sort the array, uses the inner class EdgeComparer defined below.
            Array.Sort(eArray, new EdgeComparer());

            int TreeTo = 0; //Index of the TreeTo in the forest
            int TreeFrom = 0; //Index of the TreeFrom in the Forest
            int iCurEdge = 0; //Index of the Current Edge in the edge array

            //Put the trees (new graph with a single vertex) in the forest
            for (int i = 0; i < Forest.Length; i++)
            {
                Forest[i] = (AGraph<T>)GetType().Assembly.CreateInstance(this.GetType().FullName);
                Forest[i].AddVertex(vertices[i].Data);
            }

            //While there is more than one tree in the forest
                //Do all logic here
            while(Forest.Length > 1)
            {
                //Look at the top element in the edge array
                Vertex<T> eFrom = eArray[iCurEdge].From;
                Vertex<T> eTo = eArray[iCurEdge].To;
                //Look to see if they are in different forests
                TreeFrom = FindTree(eFrom, Forest);
                TreeTo = FindTree(eTo, Forest);
                //if they are, move the higher index on to the lower index one
                if (TreeFrom != TreeTo)
                {
                    if (TreeFrom > TreeTo)
                    {
                        Forest[TreeTo].MergeTrees(Forest[TreeFrom]);
                        Forest[TreeTo].AddEdge(eFrom.Data, eTo.Data, eArray[iCurEdge].Weight);
                        Forest = Timber(TreeFrom, Forest);
                    }
                    else
                    {
                        Forest[TreeFrom].MergeTrees(Forest[TreeTo]);
                        Forest[TreeFrom].AddEdge(eFrom.Data, eTo.Data, eArray[iCurEdge].Weight);
                        Forest = Timber(TreeTo, Forest);
                    }
                }
                iCurEdge++;
            }

            //Return the remaining graph in the forest
            return Forest[0];
        }

        /// <summary>
        /// Remove a tree (graph) from the forest
        /// </summary>
        /// <param name="iTree">Index of the tree to remove</param>
        /// <param name="originalForest">The original forest</param>
        /// <returns>A new forest, with desired graph removed</returns>
        private AGraph<T>[] Timber(int iTree, AGraph<T>[] originalForest)
        {
            AGraph<T>[] newForest = new AGraph<T>[originalForest.Length - 1];
            int newIndex = 0;
            for (int i = 0; i < originalForest.Length; i++)
            {
                if( i != iTree )
                {
                    newForest[newIndex] = originalForest[i];
                    newIndex++;
                }
            }
            return newForest;
        }

        /// <summary>
        /// Merge the calling tree with the passed in tree
        /// </summary>
        /// <param name="TreeFrom">Tree to merge into the calling tree "this"</param>
        private void MergeTrees(AGraph<T> TreeFrom)
        {
            foreach(var element in TreeFrom.vertices)
            {
                this.AddVertex(element.Data);
            }

            foreach (var element in TreeFrom.GetAllEdges())
            {
                this.AddEdge(element.To.Data, element.From.Data, element.Weight);
            }
        }

        /// <summary>
        /// Finds the location of a vertex in the forest.
        /// </summary>
        /// <param name="v">Vertex to find in the forest</param>
        /// <param name="Forest">An array of graphs (trees)</param>
        /// <returns>Location of the vertices tree in the forest. Throw an exception if not found.</returns>
        private int FindTree(Vertex<T> v, AGraph<T>[] Forest)
        {
            int index = 0;
            foreach (var tree in Forest)
            {
                if (tree.HasVertex(v.Data))
                {
                    return index;
                }
                index++;
            }

            throw new ApplicationException("Not found in tree");
        }

        /// <summary>
        /// Used to compare two Edge objects
        /// Can be used by the Array.Sort method
        /// </summary>
        private class EdgeComparer : IComparer<Edge<T>>
        {
            public int Compare(Edge<T> x, Edge<T> y)
            {
                return x.CompareTo(y);
            }
        }

        public void RemoveVertex(T data)
        {
            //If vertex exists
            //  Remove vertex from vertices array
            //  Remove vertex from dictionary
            //  decrement indices (stored in dictionary and vertice objects) for all vertices "After" removes vertice
            //  Remove all edges that reference this vertex
            //else
            //  throw exception

///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//////////////                                My code                                        //////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            //if(HasVertex(data))
            //{
            //    int index = GetVertex(data).Index;
            //    vertices.Remove(GetVertex(data));
            //    bool found = false;
                
            //    foreach(T key in revLookUp.Keys.ToList<T>())
            //    {
            //        if(!found)
            //        {
            //            found = (key.CompareTo(data) == 0) ? true : false;
            //        }
            //        else
            //        {
            //            revLookUp[key]--;
            //            GetVertex(key).Index--;
            //        }
            //    }

            //    revLookUp.Remove(data);
            //}
            //else
            //{
            //    throw new ApplicationException("Vertex doesn't exist");
            //}

///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            
            //Rob's code
            Vertex<T> v = GetVertex(data);
            vertices.Remove(v);
            revLookUp.Remove(data);
            //Loop through vertices below removed vertex
            for (int i = v.Index; i < vertices.Count; i++)
            {
                //Update the current vertex object
                vertices[i].Index--;
                //Update the current vertex object's index in the dictionary
                revLookUp[vertices[i].Data]--;
            }

            RemoveVertexAdjustEdges(v);
        }

        #region Shortest Path Code
        public IGraph<T> ShortestWeightedPath(T start, T end)
        {
            //Array of VertexData objects, one for each vertex in the graph
            VertexData[] vTable = new VertexData[vertices.Count];
            //Index of starting point
            int iStartIndex = GetVertex(start).Index;

            //Load vTable with vertices
            for (int i = 0; i < vertices.Count; i++)
            {
                //Pass in the vertex, tentative distance, previous (and default for known).
                vTable[i] = new VertexData(vertices[i], double.PositiveInfinity, null);
            }

            //Set the start vertex data's distance to 0.
            vTable[iStartIndex].Distance = 0;

            //Create the priority quueu
            PriorityQueue pq = new PriorityQueue();
            //Enqueue the start vertex
            pq.Enqueue(vTable[iStartIndex]);

            /*
            while there are still vertices on the priority queue
                current <-- pq dequeue
                if current is unknown
                    set current to known
                    foreach neighbour(w) of current
                        w <-- Get the vertex data object 
                        get the edge from current to w (need it's weight)
                        proposedDistance = current's distance + edge's distance
                        if w's distance > proposedDistance
                            w's distance <-- proposedDistance
                            w's previous <-- current
                            pq enqueue w
            return BuildGraph(endVertex, vTable)
             */
            VertexData vd = null;

            while(!pq.IsEmpty())
            {
                vd = pq.Dequeue();
                if(!vd.Known)
                {
                    vd.Known = true;
                    foreach (Vertex<T> item in EnumerateNeighbours(vd.vVertex.Data))
                    {
                        //Getting the distance of the item
                        double wDistance = GetEdge(vd.vVertex.Data, item.Data).Weight;
                        //Creating the VertexData object
                        VertexData w = vTable[item.Index];
                        double proposedDistance = wDistance + vd.Distance;

                        if (w.Distance > proposedDistance)
                        {
                            w.Distance = proposedDistance;
                            w.vPrevious = vd.vVertex;
                            pq.Enqueue(w);
                            //Only enqueue it when it's been upgraded

                        }
                    }
                }
            }
            int iEndIndex = GetVertex(end).Index;
            return BuildGraph(vTable[iEndIndex].vVertex, vTable);
        }

        private IGraph<T> BuildGraph(Vertex<T> vEnd, VertexData[] vTable)
        {
            //Instantiate an instance of the child type graph using reflection
            IGraph<T> result = (IGraph<T>)GetType().Assembly.CreateInstance(this.GetType().FullName);
            /*
            add the end vertex to result
            dataLast <-- vTable(location of the vEnd)
            previous <-- previous of dataLast
            while previous is not null
                add previous to result
                add the edge from last and previous
                dataLast <-- vTable(location of previous)
                previous <-- dataLast
             */
            result.AddVertex(vEnd.Data);
            VertexData dataLast = vTable[vEnd.Index];
            Vertex<T> previous = dataLast.vPrevious;
            while(previous != null)
            {
                result.AddVertex(previous.Data);
                result.AddEdge(dataLast.vVertex.Data, previous.Data);
                dataLast = vTable[previous.Index];
                previous = dataLast.vPrevious;
            }
            return result;
        }

        internal class PriorityQueue
        {
            private List<VertexData> sl;
            public PriorityQueue()
            {
                sl = new List<VertexData>();
            }

            internal void Enqueue(VertexData vData)
            {
                sl.Add(vData);
                sl.Sort();
            }

            internal VertexData Dequeue()
            {
                VertexData RetVal = sl[0];
                sl.RemoveAt(0);
                return RetVal;
            }

            public bool IsEmpty()
            {
                return sl.Count == 0;
            }

            /// <summary>
            /// REMOVE AFTER TESTING
            /// </summary>
            public void DisplayQueue()
            {
                foreach(VertexData v in sl)
                {
                    Console.WriteLine(v.ToString());
                }
                Console.WriteLine();
            }
        }

        internal class VertexData : IComparable
        {
            public Vertex<T> vVertex;
            public double Distance;
            public Vertex<T> vPrevious;
            public bool Known;

            public VertexData(Vertex<T> vVertex, double distance, Vertex<T> vPrevious, bool known = false)
            {
                this.vVertex = vVertex;
                this.Distance = distance;
                this.vPrevious = vPrevious;
                this.Known = known;
            }

            public override string ToString()
            {
                return "Vertex: " + vVertex +" Disance: " + Distance +  " Previous: " + vPrevious.Data;
            }
            public int CompareTo(object obj)
            {
                return this.Distance.CompareTo((((VertexData)obj)).Distance);
            }
        }
        #endregion

        Edge<T> IGraph<T>.GetEdge(T from, T to)
        {
            throw new NotImplementedException();
        }

        public void BreadthFirstTraversal(T start, VisitorDelegate<T> whatToDo)
        {
            //Get the vertex object associated with the start item.
            Vertex<T> vStart = GetVertex(start);
            //Current vertex to process
            Vertex<T> vCurrent;
            //Used to track vertices already visited
            Dictionary<T, T> visitedVertices = new Dictionary<T, T>();
            //queue that holds the vertices that potentially need to be processed.
            Queue<Vertex<T>> verticesRemaining = new Queue<Vertex<T>>();
            //Push the starting vertex onto the queue
            verticesRemaining.Enqueue(vStart);

            //while there are items on the queue
            //    vCurrent <-- queue.dequeue
            //    if the current vertex has not been visited
            //        process the current vertex (call the delegate)
            //        add the current vertex to the visited list
            //        foreach neighbor of the current vertex
            //            enqueue the neighbor onto the queue
            while (verticesRemaining.Count > 0)
            {
                vCurrent = verticesRemaining.Dequeue();
                if (!visitedVertices.ContainsKey(vCurrent.Data))
                {
                    whatToDo(vCurrent.Data);
                    visitedVertices.Add(vCurrent.Data, vCurrent.Data);
                    foreach (var neighbor in this.EnumerateNeighbours(vCurrent.Data))
                    {
                        verticesRemaining.Enqueue(neighbor);
                    }
                }
            }
        }

        public void DepthFirstTraversal(T start, VisitorDelegate<T> whatToDo)
        {
            //Get the vertex object associated with the start item.
            Vertex<T> vStart = GetVertex(start);
            //Current vertex to process
            Vertex<T> vCurrent;
            //Used to track vertices already visited
            Dictionary<T, T> visitedVertices = new Dictionary<T, T>();
            //Stack that holds the vertices that potentially need to be processed.
            Stack<Vertex<T>> verticesRemaining = new Stack<Vertex<T>>();
            //Push the starting vertex onto the stack
            verticesRemaining.Push(vStart);

            //while there are items on the stack
            //    vCurrent <-- Stack.pop
            //    if the current vertex has not been visited
            //        process the current vertex (call the delegate)
            //        add the current vertex to the visited list
            //        foreach neighbor of the current vertex
            //            push the neighbor onto the stack
            while (verticesRemaining.Count > 0)
            {
                vCurrent = verticesRemaining.Pop();
                if (!visitedVertices.ContainsKey(vCurrent.Data))
                {
                    whatToDo(vCurrent.Data);
                    visitedVertices.Add(vCurrent.Data, vCurrent.Data);
                    foreach (var neighbor in this.EnumerateNeighbours(vCurrent.Data))
                    {
                        verticesRemaining.Push(neighbor);
                    }
                }
            }
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            //Loop through each vertice and add to the result
            foreach (Vertex<T> v in EnumerateVertices())
            {
                result.Append(v + ", ");
            }
            //Take off the last comma
            if (vertices.Count > 0)
            {
                result.Remove(result.Length - 2, 2);
            }
            return GetType().Name + "\nVertices: " + result + "\n";
        }
    }
}
