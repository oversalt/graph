using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph
{
    //A delegate used to process a vertex
    public delegate void VisitorDelegate<T>(T data);
    public interface IGraph<T> where T: IComparable<T>
    {
        #region Properties
        int NumVertices { get; }
        int NumEdges { get; }
        #endregion

        #region Methods to work with Vertices
        void AddVertex(T data);
        void RemoveVertex(T data);
        Vertex<T> GetVertex(T data);
        bool HasVertex(T data);
        IEnumerable<Vertex<T>> EnumerateVertices();
        IEnumerable<Vertex<T>> EnumerateNeighbours(T data);
        #endregion

        #region Methods to work with Edges
        void AddEdge(T from, T to);
        void AddEdge(T from, T to, double weight);
        bool HasAnEdge(T from, T to);
        Edge<T> GetEdge(T from, T to);
        void RemoveEdge(T from, T to);
        #endregion

        #region Implementation of Algorithms that do Graph type work
        IGraph<T> ShortestWeightedPath(T start, T end);
        IGraph<T> MinimumSpanningTree();
        void DepthFirstTraversal(T start, VisitorDelegate<T> whatToDo);
        void BreadthFirstTraversal(T start, VisitorDelegate<T> whatToDo);
        #endregion
    }
}
