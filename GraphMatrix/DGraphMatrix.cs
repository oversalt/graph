using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Graph;

namespace GraphMatrix
{
    //Directed Graph --> An edge from A to B is stored as a single edge in the matrix.
    //Indicates that we can travel from A to B, but not necessarily from B to A.
    public class DGraphMatrix<T>: AGraphMatrix<T> where T: IComparable<T>
    {
        public DGraphMatrix()
        {
            //Defined in AGraph
            isDirected = true;
        }

        protected override Edge<T>[] GetAllEdges()
        {
            List<Edge<T>> edges = new List<Edge<T>>();
            //Visit every row
            for (int r = 0; r < matrix.GetLength(0); r++)
            {
                for (int c = 0; c < matrix.GetLength(1); c++)
                {
                    if (matrix[r, c] != null)
                    {
                        edges.Add(matrix[r, c]);
                    }
                }
            }

            return edges.ToArray();
        }

    }
}
