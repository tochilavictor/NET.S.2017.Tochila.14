﻿using System;
using System.Collections.Generic;
using System.Linq;
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
        public IEnumerable<int> Fibonacci(int n)
        {
            if(n<1) throw new ArgumentOutOfRangeException($"{nameof(n)} must be natural");
            int prevfib = 0;
            int curfib = 1;
            int i = 0;
            while (i < n)
            {
                yield return prevfib;
                int tmp = curfib;
                curfib = prevfib + curfib;
                prevfib = tmp;
                i++;
            }
        }
    }
}
