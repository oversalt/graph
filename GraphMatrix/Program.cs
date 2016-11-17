using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Graph;

namespace GraphMatrix
{
    class Program
    {
        public static void whatToDo(string data)
        {
            Console.WriteLine("Visiting --> " + data.ToString());
        }

        static void TestTraversals()
        {
            UGraphMatrix<string> uGraph = new UGraphMatrix<string>();
            uGraph.AddVertex("PA");
            uGraph.AddVertex("Saskatoon");
            uGraph.AddVertex("Regina");
            uGraph.AddVertex("Weyburn");
            uGraph.AddVertex("Estevan");
            uGraph.AddVertex("MJ");
            uGraph.AddVertex("Yorkton");
            uGraph.AddVertex("Swift");

            uGraph.AddEdge("PA", "Saskatoon", 141);
            uGraph.AddEdge("Saskatoon", "MJ", 220);
            uGraph.AddEdge("Saskatoon", "Yorkton", 328);
            uGraph.AddEdge("Yorkton", "Regina", 187);
            uGraph.AddEdge("Swift", "MJ", 190);
            uGraph.AddEdge("MJ", "Regina", 72);
            uGraph.AddEdge("Regina", "Weyburn", 115);
            uGraph.AddEdge("Weyburn", "Estevan", 86);

            Console.WriteLine("Depth First:");
            uGraph.DepthFirstTraversal("PA", whatToDo);
            Console.WriteLine("\nBreadth First:");
            uGraph.BreadthFirstTraversal("PA", whatToDo);
        }


        static void TestDirectedGraph()
        {
            DGraphMatrix<string> dGraph = new DGraphMatrix<string>();
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

        static void TestUndirectedGraph()
        {
            UGraphMatrix<string> uGraph = new UGraphMatrix<string>();
            uGraph.AddVertex("Saskatoon");
            uGraph.AddVertex("Moose Jaw");
            uGraph.AddVertex("Regina");
            uGraph.AddEdge("Moose Jaw", "Regina", 235);
            uGraph.AddEdge("Moose Jaw", "Saskatoon", 250);
            uGraph.AddEdge("Saskatoon", "Regina", 70);
            //uGraph.RemoveEdge("Saskatoon", "Regina");
            Console.WriteLine(uGraph);

            //List<Vertex<string>> myL = (List<Vertex<string>>)uGraph.EnumerateNeighbours("Moose Jaw");
            //myL.Remove(uGraph.GetVertex("Saskatoon"));
            //foreach(Vertex<string> v in myL)
            //{
            //    Console.WriteLine(v.Data);
            //}
            Edge<string>[] edges = uGraph.TestGetAllEdges();

            foreach (var e in edges)
            {
                Console.WriteLine(e.ToString());
            }
        }

        static void TestUndirectedRemoveVertex()
        {
            UGraphMatrix<string> uGraph = new UGraphMatrix<string>();
            uGraph.AddVertex("Saskatoon");
            uGraph.AddVertex("Moose Jaw");
            uGraph.AddVertex("Regina");
            uGraph.AddEdge("Saskatoon", "Moose Jaw", 235);
            uGraph.AddEdge("Saskatoon", "Regina", 250);
            uGraph.AddEdge("Regina", "Moose Jaw", 70);
            Console.WriteLine(uGraph);

            uGraph.RemoveVertex("Saskatoon");

            Console.WriteLine(uGraph);
        }

        static void TestShortestWeightedPath()
        {
            UGraphMatrix<string> uGraph = new UGraphMatrix<string>();
            uGraph.AddVertex("Prince Albert");
            uGraph.AddVertex("Saskatoon");
            uGraph.AddVertex("Yorkton");
            uGraph.AddVertex("Regina");
            uGraph.AddVertex("Weyburn");
            uGraph.AddEdge("Prince Albert", "Saskatoon", 2);
            uGraph.AddEdge("Saskatoon", "Yorkton", 4);
            uGraph.AddEdge("Saskatoon", "Regina", 1);
            uGraph.AddEdge("Regina", "Yorkton", 3);
            uGraph.AddEdge("Regina", "Weyburn", 5);
            uGraph.AddEdge("Yorkton", "Weyburn", 1);

            Console.WriteLine(uGraph);

            Console.WriteLine(uGraph.ShortestWeightedPath("Weyburn", "Prince Albert"));

        }

        static void Main(string[] args)
        {
            //TestDirectedGraph();
            //TestUndirectedGraph();
            //TestUndirectedRemoveVertex();
            //TestTraversals();
            TestShortestWeightedPath();
        }
    }
}
