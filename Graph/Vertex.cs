using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph
{
    /// <summary>
    /// This class represents a single node in the graph. For example,
    /// a city, computer, person, etc.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Vertex<T> where T: IComparable<T>
    {
        #region Attributes
        private T data;
        private int index;
        #endregion

        #region Constructors
        public Vertex(int index, T data)
        {
            this.index = index;
            this.data = data;
        }
        #endregion

        #region Properties
        public int Index
        {
            get { return index; }
            set { index = value; }
        }
        public T Data
        {
            get { return data; }
        }
        #endregion

        /// <summary>
        /// Compares the indices of two vertices.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(Vertex<T> other)
        {
            return Index.CompareTo(other.index);
        }

        public override string ToString()
        {
            return "[" + data + "(" + index + ")]";
        }

        public override bool Equals(object obj)
        {
            return this.CompareTo((Vertex<T>)obj) == 0;
        }
    }
}
