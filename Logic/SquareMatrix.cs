using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Logic
{
    public class MatrixEventArgs<T> : EventArgs
    {
        private readonly T oldvalue;
        private readonly T newvalue;

        public MatrixEventArgs(T oldv,T newv)
        {
            oldvalue = oldv;
            newvalue = newv;
        }
        public T OldValue => oldvalue;
        public T NewValue => newvalue;
        
    }

    public class SquareMatrix<T>:IEnumerable<T>
    {
        public event EventHandler<MatrixEventArgs<T>> ValueChanged;
        private T[][] matrix;
        public SquareMatrix(int order)
        {
            matrix = new T[order][];
            for (int i = 0; i < order; i++)
            {
                matrix[i] = new T[order];
            }
        }
        public int Order => matrix.Length;

        protected virtual void OnValueChanged(MatrixEventArgs<T> args)
        {
            ValueChanged?.Invoke(this, args);
        }

        public T[] this[int i]
        {
            get
            {
                if (i < 1 || i > matrix.Length) throw new ArgumentOutOfRangeException();
                return matrix[i - 1];
            }
        }

        public virtual T this[int i, int j]
        {
            get
            {
                if (i < 1 || i > matrix.Length) throw new ArgumentOutOfRangeException();
                if (j < 1 || j > matrix.Length) throw new ArgumentOutOfRangeException();
                return matrix[i - 1][j - 1];
            }
            set
            {
                if (i < 1 || i > matrix.Length) throw new ArgumentOutOfRangeException();
                if (j < 1 || j > matrix.Length) throw new ArgumentOutOfRangeException();
                if (value.Equals(matrix[i - 1][j - 1])) return;
                OnValueChanged(new MatrixEventArgs<T>(matrix[i - 1][j - 1],value));
                matrix[i - 1][j - 1] = value;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (T[] matrixstring in matrix)
            {
                foreach (T element in matrixstring)
                {
                    yield return element;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
