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
            
        }

        public override Edge<T> GetEdge(T from, T to)
        {
            if (!HasEdge(from, to))
            {
                throw new ApplicationException("No edge exists");
            }

            return listListEdges.;
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        protected override void RemoveVertexAdjustEdges(Vertex<T> v)
        {
            throw new NotImplementedException();
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
