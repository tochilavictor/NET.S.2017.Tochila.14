using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    class SymmetricMatrix<T> : SquareMatrix<T>
    {
        public SymmetricMatrix(int order) : base(order, order, order + (order * order - order) / 2) { }
        protected SymmetricMatrix(int rows, int columns, int size) : base(rows, columns, size) { }
        public int GetOffset(int i, int j)
        {
            int res = 0;
            for (int r = 1; r <= i; r++)
            {
                for (int c = 1; c <= j; c++)
                {
                    if (r > c) res++;
                }
            }
            return res;
        }
        public override T this[int i, int j]
        {
            get
            {
                if (i > j)
                {
                    int tmp = i;
                    i = j;
                    j = tmp;
                }
                int offset = GetOffset(i, j);
                int joffset = offset % N;
                int ioffset = (offset - joffset) / N;
                return base[(i - ioffset), (j - joffset)];
            }
            set
            {
                if (i > j)
                {
                    int tmp = i;
                    i = j;
                    j = tmp;
                }
                int offset = GetOffset(i, j);
                int joffset = offset % N;
                int ioffset = (offset - joffset) / N;
                base[(i - ioffset), (j - joffset)] = value;
            }
        }
    }
}
