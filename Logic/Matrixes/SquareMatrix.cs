    using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
    using Logic.Matrixes;

namespace Logic
{
    public class SquareMatrix<T> : AbstractMatrix<T>
    {
        private int order;
        public SquareMatrix(int order) : base(order, order, order * order)
        {
            this.order = order;
        }

        protected SquareMatrix(int rows, int columns, int size) : base(rows, columns, size) { }

        public int Order => order;
    }
}
