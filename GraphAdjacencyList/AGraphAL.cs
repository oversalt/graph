using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Graph;

namespace GraphAdjacencyList
{
    public abstract class AGraphAL<T> : AGraph<T> where T : IComparable<T>
    {
        #region Attributes
        protected List<List<Edge<T>>> listListEdges;

        #endregion

        public AGraphAL()
        {
            listListEdges = new List<List<Edge<T>>>();
        }

        public override IEnumerable<Vertex<T>> EnumerateNeighbours(T data)
        {
            List<Vertex<T>> neighbours = new List<Vertex<T>>();

            foreach(var edge in listListEdges[GetVertex(data).Index])
            {
                neighbours.Add(edge.To);
            }
            return neighbours;
        }

        public override Edge<T> GetEdge(T from, T to)
        {
            if (!HasEdge(from, to))
            {
                throw new ApplicationException("No edge exists");
            }

            foreach(var e in listListEdges[GetVertex(from).Index])
            {
                if(e.To.Data.CompareTo(to) == 0)
                {
                    return e;
                }
            }

            throw new ApplicationException("Does not contain that edge");
        }

        public override bool HasEdge(T from, T to)
        {
            try
            {
                //How does contains compare two items?
                return listListEdges[GetVertex(from).Index].Contains(new Edge<T>(GetVertex(from), GetVertex(to)));
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public override void RemoveEdge(T from, T to)
        {
            if(HasEdge(from, to))
            {
                listListEdges[GetVertex(from).Index].Remove(GetEdge(from, to));
                numEdges--;
            }
            else
            {
                throw new ApplicationException("Graph doesn't contain that edge.");
            }
        }

        protected override void AddEdge(Edge<T> e)
        {
            //If the edge already exists, throw an exception
            if (HasEdge(e.From.Data, e.To.Data))
            {
                throw new ApplicationException("Edge already exists");
            }
            //Add an edge to the list of edges for the from vertex
            listListEdges[e.From.Index].Add(e);
            //Increment the edge count
            numEdges++;
        }

        protected override void AddVertexAdjustEdges(Vertex<T> v)
        {
            //Add a new list of edges to the list of lists of Edges of type T
            listListEdges.Add(new List<Edge<T>>());
        }

        protected override Edge<T>[] GetAllEdges()
        {
            List<Edge<T>> eArray = new List<Edge<T>>();
            foreach (var lEdge in listListEdges)
            {
                foreach(var e in lEdge)
                {
                    eArray.Add(e);
                }
            }
            return eArray.ToArray();
        }

        protected override void RemoveVertexAdjustEdges(Vertex<T> v)
        {
            numEdges = 0;

            for (int i = 0; i < vertices.Count; i++)
            {
                if(i != v.Index)
                {
                    for(int x = 0; x < vertices.Count; x++)
                    {
                        if (listListEdges[i][x].From.CompareTo(v) == 0 || listListEdges[i][x].To.CompareTo(v) == 0)
                        {
                            listListEdges[i].RemoveAt(x);   //Removing any edges that might exist with the vertex
                        }
                    }
                }
                else
                {
                    listListEdges.RemoveAt(i);  //Removing the list of edges at the vertex's index
                }
            }
        }

        public override string ToString()
        {
            StringBuilder sbEdges = new StringBuilder("Edges:\n");
            for (int r = 0; r < listListEdges.Count; r++)
            {
                sbEdges.Append("Index " + r + ": ");
                bool commaAdded = false;
                foreach (Edge<T> e in listListEdges[r])
                {
                    sbEdges.Append(e + ", ");
                    commaAdded = true;
                }
                if (commaAdded)
                {
                    sbEdges.Remove(sbEdges.Length - 2, 2);
                }
                sbEdges.Append("\n");
            }
            return base.ToString() + sbEdges;
        }
    }
}
