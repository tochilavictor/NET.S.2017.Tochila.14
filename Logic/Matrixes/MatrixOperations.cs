using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Matrixes
{
    public class MatrixOperations
    {
        /// <summary>
        /// implementation of addition operation for 2 squarematrix
        /// </summary>
        /// <typeparam name="T">type parameter</typeparam>
        /// <param name="lhs">first matrix</param>
        /// <param name="rhs">second matrix</param>
        /// <returns>matrix, which is result of addition first and second</returns>
        public static SquareMatrix<T> Add<T>(SquareMatrix<T> lhs, SquareMatrix<T> rhs)
        {
            if (ReferenceEquals(lhs, null) || ReferenceEquals(rhs, null)) throw new ArgumentNullException();
            if (lhs.M != rhs.M) throw new ArgumentException($"you can composit only same-order matrixes");

            ParameterExpression paramlhs = Expression.Parameter(typeof(T), "lhs");
            ParameterExpression paramrhs = Expression.Parameter(typeof(T), "rhs");
            BinaryExpression addition = Expression.Add(paramlhs, paramrhs);
            Expression<Func<T, T, T>> expressionAdd = Expression.Lambda<Func<T, T, T>>(addition, paramlhs, paramrhs);
            Func<T, T, T> functionAdd = expressionAdd.Compile();

            return MatrixAddition((dynamic)lhs, (dynamic)rhs, functionAdd);
        }

        private static SquareMatrix<T> MatrixAddition<T>(SquareMatrix<T> lhs, SquareMatrix<T> rhs, Func<T, T, T> addition)
        {
            SquareMatrix<T> result = new SquareMatrix<T>(lhs.M);

            for (int i = 1; i <= lhs.M; i++)
            {
                for (int j = 1; j <= lhs.M; j++)
                {
                    result[i, j] = addition(lhs[i, j], rhs[i, j]);
                }
            }
            return result;
        }

        private static DiagonalMatrix<T> MatrixAddition<T>(DiagonalMatrix<T> lhs, DiagonalMatrix<T> rhs, Func<T, T, T> addition)
        {

            DiagonalMatrix<T> result = new DiagonalMatrix<T>(lhs.M);

            for (int i = 1; i <= lhs.M; i++)
            {
                result[i, i] = addition(lhs[i, i], rhs[i, i]);
            }
            return result;
        }

        private static SymmetricMatrix<T> MatrixAddition<T>(SymmetricMatrix<T> lhs, SymmetricMatrix<T> rhs, Func<T, T, T> addition)
        {

            SymmetricMatrix<T> result = new SymmetricMatrix<T>(lhs.M);
            for (int i = 1; i <= lhs.M; i++)
            {
                for (int j = 1; j <= i; j++)
                {
                    result[i, j] = addition(lhs[i, j], rhs[i, j]);
                }
            }
            return result;
        }

        private static SymmetricMatrix<T> MatrixAddition<T>(SymmetricMatrix<T> lhs, DiagonalMatrix<T> rhs, Func<T, T, T> addition)
        {
            SymmetricMatrix<T> result = new SymmetricMatrix<T>(lhs.M);
            for (int i = 1; i <= lhs.M; i++)
            {
                for (int j = 1; j <= i; j++)
                {
                    result[i, j] = lhs[i, j];
                }
            }
            for (int i = 1; i <= lhs.M; i++)
            {
                result[i, i] = addition(lhs[i, i], rhs[i, i]);
            }
            return result;
        }
        private static SymmetricMatrix<T> MatrixAddition<T>(DiagonalMatrix<T> lhs, SymmetricMatrix<T> rhs, Func<T, T, T> addition)
        {
            return MatrixAddition(rhs, lhs, addition);
        }

    }
}
