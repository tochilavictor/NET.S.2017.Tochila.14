using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    class DiagonalMatrix<T> : SquareMatrix<T>
    {
        public DiagonalMatrix(int order) : base(order, order, order) { }
        protected DiagonalMatrix(int rows, int columns, int size) : base(rows, columns, size) { }

        public override T this[int i, int j]
        {
            get
            {
                ValidateIndexes(i, j);
                if (i != j) return default(T);
                return base[1, j];
            }
            set
            {
                ValidateIndexes(i, j);
                if (i != j) throw new ArgumentOutOfRangeException($"you can update only diagonal elements");
                base[1, j] = value;
            }
        }
        public IEnumerable<T> DiagonalElementsOnly()
        {
            for (int i = 1; i <= N; i++)
            {
                yield return this[i, i];
            }
        }
    }
}
