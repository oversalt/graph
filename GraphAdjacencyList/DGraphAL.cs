using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Graph;

namespace GraphAdjacencyList
{
    public class DGraphAL<T>: AGraphAL<T> where T: IComparable<T>
    {
        public DGraphAL()
        {
            isDirected = true;
        }
    }
}
