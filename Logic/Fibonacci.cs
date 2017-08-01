using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class Fibonacci
    {
        /// <summary>
        /// imlementation of Fibonacci numbers generator
        /// </summary>
        /// <param name="n">index of last number</param>
        /// <returns>enumeration of fibonacci numbers</returns>
        public static IEnumerable<BigInteger> FibonacciNumbers(int n)
        {
            if (n < 1) throw new ArgumentOutOfRangeException($"{nameof(n)} must be natural");
            return FibonacciGenerator(n);
        }
        private static IEnumerable<BigInteger> FibonacciGenerator(int n)
        {
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
    }
}
