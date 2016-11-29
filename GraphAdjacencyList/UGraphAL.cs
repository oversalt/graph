using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Graph;

namespace GraphAdjacencyList
{
    public class UGraphAL<T>: AGraphAL<T> where T : IComparable<T>
    {
        public UGraphAL()
        {
            isDirected = false;
        }

        //Override numEdges since for every logical edge added, our code adds 2 edges,
        //one in each direction.
        public override int NumEdges
        {
            get
            {
                return base.NumEdges / 2;
            }
        }


        public override void AddEdge(T from, T to)
        {
            //Add two edges A --> B and B --> A
            base.AddEdge(from, to);
            base.AddEdge(to, from);
        }

        public override void AddEdge(T from, T to, double weight)
        {
            //Add two edges A --> B and B --> A
            base.AddEdge(from, to, weight);
            base.AddEdge(to, from, weight);
        }

        public override void RemoveEdge(T from, T to)
        {
            base.RemoveEdge(from, to);
            base.RemoveEdge(to, from);
        }
    }
}
