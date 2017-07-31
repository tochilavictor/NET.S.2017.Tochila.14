using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Matrixes
{
    public class MatrixEventArgs<T> : EventArgs
    {
        private readonly T oldvalue;
        private readonly T newvalue;
        private readonly int i;
        private readonly int j;
        public MatrixEventArgs(T oldv, T newv, int row, int column)
        {
            oldvalue = oldv;
            newvalue = newv;
            i = row;
            j = column;
        }
        public T OldValue => oldvalue;
        public T NewValue => newvalue;
        public int I => i;
        public int J => j;

    }
    public abstract class AbstractMatrix<T> : IEnumerable<T>
    {
        private readonly T[] matrix;
        public virtual int N { get; }
        public virtual int M { get; }

        public AbstractMatrix(int rows, int columns, int size)
        {
            if (rows < 1 || columns < 1 || size < 1) throw new ArgumentOutOfRangeException();
            matrix = new T[size];
            N = rows;
            M = columns;
        }
        public event EventHandler<MatrixEventArgs<T>> ValueChanged;
        protected virtual void OnValueChanged(MatrixEventArgs<T> args)
        {
            ValueChanged?.Invoke(this, args);
        }

        public virtual T this[int i, int j]
        {
            get
            {
                ValidateIndexes(i, j);
                return matrix[(i - 1) * N + j - 1];
            }
            set
            {
                ValidateIndexes(i, j);
                OnValueChanged(new MatrixEventArgs<T>(matrix[(i - 1) * N + j - 1], value, i, j));
                matrix[(i - 1) * N + j - 1] = value;
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            for (int i = 1; i <= N; i++)
            {
                for (int j = 1; j <= M; j++)
                {
                    if (this[i, j] == null) sb.Append("null".PadRight(5));
                    else sb.Append(this[i, j].ToString().PadRight(5));
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }

        protected bool ValidateIndexes(int i, int j)
        {
            if (i < 1 || i > N) throw new ArgumentOutOfRangeException();
            if (j < 1 || j > M) throw new ArgumentOutOfRangeException();
            return true;
        }
        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 1; i <= N; i++)
            {
                for (int j = 1; j <= M; j++)
                {
                    yield return this[i, j];
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
