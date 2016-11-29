using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphAdjacencyList
{
    class Program
    {
        static void TestDirectedGraph()
        {
            DGraphAL<string> dGraph = new DGraphAL<string>();
            dGraph.AddVertex("Saskatoon");
            dGraph.AddVertex("Moose Jaw");
            dGraph.AddVertex("Regina");
            dGraph.AddEdge("Saskatoon", "Moose Jaw", 235);
            dGraph.AddEdge("Saskatoon", "Regina", 250);
            dGraph.AddEdge("Regina", "Moose Jaw", 70);
            //dGraph.RemoveEdge("Saskatoon", "Regina");
            Console.WriteLine(dGraph);

            //List<Vertex<string>> myL = (List<Vertex<string>>)uGraph.EnumerateNeighbours("Regina");

            //foreach (Vertex<string> v in myL)
            //{
            //    Console.WriteLine(v.Data);
            //}

            //Edge<string>[] edges = Graph.TestGetAllEdges();

            //foreach(var e in edges)
            //{
            //    Console.WriteLine(e.ToString());
            //}
        }

        static void TestUndirectedRemoveVertex()
        {
            UGraphAL<string> uGraph = new UGraphAL<string>();
            uGraph.AddVertex("Saskatoon");
            uGraph.AddVertex("Moose Jaw");
            uGraph.AddVertex("Regina");
            uGraph.AddEdge("Saskatoon", "Moose Jaw", 235);
            uGraph.AddEdge("Saskatoon", "Regina", 250);
            uGraph.AddEdge("Regina", "Moose Jaw", 70);
            Console.WriteLine(uGraph);

            Console.WriteLine(uGraph.GetEdge("Saskatoon", "Moose Jaw"));

            Console.WriteLine(uGraph);
            Console.WriteLine(uGraph.NumEdges);
        }
        


        static void Main(string[] args)
        {
            //TestDirectedGraph();
            TestUndirectedRemoveVertex();
        }
    }
}
