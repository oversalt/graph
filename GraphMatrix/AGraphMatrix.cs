using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Graph;

namespace GraphMatrix
{
    public abstract class AGraphMatrix<T>: AGraph<T> where T: IComparable<T>
    {
        #region Attributes
        protected Edge<T>[,] matrix;
        #endregion

        #region Constructor
        public AGraphMatrix()
        {
            matrix = new Edge<T>[0, 0];
        }
        #endregion

        #region Abstract method implementations
        /// <summary>
        /// Gets a list of all Vertices that are neighbours of "data"
        /// </summary>
        /// <param name="data"></param>
        /// <returns>Returns a List<> collection.</returns>
        public override IEnumerable<Vertex<T>> EnumerateNeighbours(T data)
        {
            List<Vertex<T>> neighbours = new List<Vertex<T>>();
            Vertex<T> v = GetVertex(data);
            //Loop through all of the neighbours and add them as vertex objects to the list
            for (int c = 0; c < matrix.GetLength(1); c++)
            {
                //If the current location is an edge
                if(matrix[v.Index, c] != null)
                {
                    //Add the "to" vertex to the list
                    neighbours.Add(matrix[v.Index, c].To);
                }
            }
            
            //try
            //{
            //    int index = vertices.IndexOf(GetVertex(data));
            //    //Grab element before the data and grab the element after the data
            //    if(index > 0)
            //    {
            //        neighbours.Add(GetVertex(matrix[index, index - 1].To.Data));
            //    }
            //    if(index < matrix.GetLength(0) - 1)
            //    {
            //        neighbours.Add(GetVertex(matrix[index, index + 1].To.Data));
            //    }
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e.Message);
            //}

            return neighbours;
        }

        public override Edge<T> GetEdge(T from, T to)
        {
            if(!HasEdge(from, to))
            {
                throw new ApplicationException("No edge exists");
            }
            return matrix[GetVertex(from).Index, GetVertex(to).Index];
        }

        public override bool HasEdge(T from, T to)
        {
            return matrix[GetVertex(from).Index, GetVertex(to).Index] != null;
        }

        public override void RemoveEdge(T from, T to)
        {
            if (HasEdge(from, to))
            {
                //Index into matrix and set location to null
                matrix[GetVertex(from).Index, GetVertex(to).Index] = null;
                numEdges--;
            }
        }

        protected override void AddEdge(Edge<T> e)
        {
            //If the edge already exists, throw an exception
            if(HasEdge(e.From.Data, e.To.Data))
            {
                throw new ApplicationException("Edge already exists");
            }
            //Index into the array and add the edge object
            matrix[e.From.Index, e.To.Index] = e;
            //Increment the edge count
            numEdges++;
        }

        protected override void AddVertexAdjustEdges(Vertex<T> v)
        {
            //Create a references to the existing matrix
            Edge<T>[,] oldMatrix = matrix;
            //Create a new, larger matrix
            matrix = new Edge<T>[NumVertices, NumVertices];
            //Copy edges from oldMatrix to new one
            for (int r = 0; r < oldMatrix.GetLength(0); r++)
            {
                for (int c = 0; c < oldMatrix.GetLength(1); c++)
                {
                    matrix[r, c] = oldMatrix[r, c];
                }
            }
        }

        protected override void RemoveVertexAdjustEdges(Vertex<T> v)
        {
            numEdges = 0;
            //Create a reference to the existing matrix
            Edge<T>[,] oldMatrix = matrix;
            //Create a new, smaller matrix
            matrix = new Edge<T>[NumVertices, NumVertices];
            //Copy edges that do not contain vertex v from oldMatrix to new one
            for (int r = 0; r < oldMatrix.GetLength(0); r++)
            {
                for (int c = 0; c < oldMatrix.GetLength(1); c++)
                {
                    if (oldMatrix[r, c] != null)
                    {
                        //If the edge does not contain vertex to remove
                        if (r != v.Index && c != v.Index)
                        {
                            Edge<T> e = oldMatrix[r, c];
                            AddEdge(e);
                        }
                    }
                }
            }
        }

        #endregion

        public override string ToString()
        {
            StringBuilder result = new StringBuilder("\nEdge Matrix:\n");
            for (int r = 0; r < matrix.GetLength(0); r++)
            {
                Vertex<T> v = vertices[r];
                result.Append(v.Data.ToString() + "\t");
                for (int c = 0; c < matrix.GetLength(1); c++)
                {
                    result.Append((matrix[r, c] == null ? "null" : matrix[r, c].To.ToString()) + "\t");
                }
                result.Append("\n");

            }
            //Return the vertices appended to the edges
            return base.ToString() + result;
        }
    }
}
