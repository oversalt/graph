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
            DGraphMatrix<string> uGraph = new DGraphMatrix<string>();
            uGraph.AddVertex("A");
            uGraph.AddVertex("B");
            uGraph.AddVertex("C");
            uGraph.AddVertex("D");
            uGraph.AddVertex("E");
            uGraph.AddVertex("F");
            uGraph.AddVertex("G");
            uGraph.AddVertex("H");
            uGraph.AddVertex("I");

            uGraph.AddEdge("A","B");
            uGraph.AddEdge("A","D");
            uGraph.AddEdge("A","E");
            uGraph.AddEdge("B","E");
            uGraph.AddEdge("C","B");
            uGraph.AddEdge("D","G"); 
            uGraph.AddEdge("E","F");
            uGraph.AddEdge("E","H");
            uGraph.AddEdge("F","C");
            uGraph.AddEdge("F","H");
            uGraph.AddEdge("G","H");
            uGraph.AddEdge("H","I");
            uGraph.AddEdge("I","F");


            Console.WriteLine("Depth First:");
            uGraph.DepthFirstTraversal("A", whatToDo);
            Console.WriteLine("\nBreadth First:");
            uGraph.BreadthFirstTraversal("B", whatToDo);
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

        static void TestMinimumSpanningTree()
        {
            UGraphMatrix<string> uGraph = new UGraphMatrix<string>();
            uGraph.AddVertex("Prince Albert");
            uGraph.AddVertex("Saskatoon");
            uGraph.AddVertex("Yorkton");
            uGraph.AddVertex("Regina");
            uGraph.AddVertex("Weyburn");
            uGraph.AddVertex("Medicine Hat");
            uGraph.AddVertex("Swift Current");
            uGraph.AddVertex("Moose Jaw");
            uGraph.AddEdge("Medicine Hat", "Swift Current", 1);
            uGraph.AddEdge("Swift Current", "Moose Jaw", 2);
            uGraph.AddEdge("Regina", "Weyburn", 2);
            uGraph.AddEdge("Medicine Hat", "Saskatoon", 3);
            uGraph.AddEdge("Prince Albert", "Saskatoon", 3);
            uGraph.AddEdge("Saskatoon", "Moose Jaw", 3);
            uGraph.AddEdge("Yorkton", "Regina", 3);
            uGraph.AddEdge("Prince Albert", "Swift Current", 4);
            uGraph.AddEdge("Moose Jaw", "Regina", 4);
            uGraph.AddEdge("Saskatoon", "Yorkton", 5);

            Console.WriteLine(uGraph);

            Console.WriteLine(uGraph.MinimumSpanningTree());

        }

        static void Main(string[] args)
        {
            //TestDirectedGraph();
            //TestUndirectedGraph();
            //TestUndirectedRemoveVertex();
            TestTraversals();
            //TestShortestWeightedPath();
            //TestMinimumSpanningTree();
        }
    }
}
