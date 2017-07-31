using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class MathOperations
    {
        /// <summary>
        /// imlementation of Fibonacci numbers generator
        /// </summary>
        /// <param name="n">index of last number</param>
        /// <returns>enumeration of fibonacci numbers</returns>
        private static IEnumerable<BigInteger> FibonacciLong(int n)
        {
            if (n < 1) throw new ArgumentOutOfRangeException($"{nameof(n)} must be natural");
            BigInteger prevfib = 0;
            BigInteger curfib = 1;
            int i = 0;
            while (i < n)
            {
                yield return prevfib;
                BigInteger tmp = curfib;
                curfib = prevfib + curfib;
                prevfib = tmp;
                i++;
            }
        }
        /// <summary>
        /// implementation of addition operation for 2 squarematrix
        /// </summary>
        /// <typeparam name="T">type parameter</typeparam>
        /// <param name="lhs">first matrix</param>
        /// <param name="rhs">second matrix</param>
        /// <returns>matrix, which is result of addition first and second</returns>
        public static SquareMatrix<T> MatrixAddition<T>(SquareMatrix<T> lhs, SquareMatrix<T> rhs)
        {
            if(ReferenceEquals(lhs,null) || ReferenceEquals(rhs,null)) throw new ArgumentNullException();
            if (lhs.Order != rhs.Order) throw new ArgumentException($"you can composit only same-order matrixes");

            ParameterExpression paramlhs = Expression.Parameter(typeof(T), "lhs");
            ParameterExpression paramrhs = Expression.Parameter(typeof(T), "rhs");
            BinaryExpression addition = Expression.Add(paramlhs, paramrhs);
            Expression<Func<T, T, T>> expressionAdd = Expression.Lambda<Func<T, T, T>>(addition, paramlhs, paramrhs);
            Func<T, T, T> functionAdd = expressionAdd.Compile();

            SquareMatrix<T> result = new SquareMatrix<T>(lhs.Order);

            for (int i = 1; i <= lhs.Order; i++)
            {
                for (int j = 1; j <= lhs.Order; j++)
                {
                    result[i, j] = functionAdd(lhs[i, j], rhs[i, j]);
                }
            }
            return result;
        }
    }
}
