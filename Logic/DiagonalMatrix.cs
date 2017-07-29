using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    class DiagonalMatrix<T> : SquareMatrix<T>
    {
        public DiagonalMatrix(int order) : base(order) { }

        public override T this[int i, int j]
        {
            get { return base[i, j]; }
            set
            {
                if(i != j) throw new ArgumentOutOfRangeException($"you can update only diagonal elements");
                base[i, j] = value;
            }
        }
    }
}
