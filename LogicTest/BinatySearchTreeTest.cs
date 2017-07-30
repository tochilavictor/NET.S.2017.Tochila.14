using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic;
using NUnit.Framework;

namespace LogicTest
{
    public class DigitsSumIntComparer : Comparer<int>
    {
        public override int Compare(int x, int y)
        {
            int sum1 = 0;
            foreach (char c in x.ToString().ToCharArray())
            {
                int k = (int)Char.GetNumericValue(c);
                sum1 += k;
            }
            int sum2 = 0;
            foreach (char c in y.ToString().ToCharArray())
            {
                int k = (int)Char.GetNumericValue(c);
                sum2 += k;
            }
            return sum1.CompareTo(sum2);
        }
    }
    [TestFixture]
    public class BinatySearchTreeTest
    {
        [TestCase(new int[] {8, 5, 12, 16, 11, 7, 4}, new int[] {8,5,4,7,12,11,16})]
        public void PreOrder_DefaultIntComparer_PositiveTest(int[] collection , int[] expresult)
        {
            BinarySearchTree<int> tree = new BinarySearchTree<int>(collection);

            int [] actResult = new int[tree.Count];
            int i = 0;
            foreach (int elem in tree)
            {
                actResult[i] = elem;
                i++;
            }

            Assert.AreEqual(expresult, actResult);
        }
        [TestCase(new int[] { 17,9,29,23,12,42 }, new int[] { 12,23,42,17,9,29 })]
        public void InOrder_DigitSumIntComparer_PositiveTest(int[] collection, int[] expresult)
        {
            BinarySearchTree<int> tree = new BinarySearchTree<int>(collection,new DigitsSumIntComparer());

            int[] actResult = new int[tree.Count];
            int i = 0;
            foreach (int elem in tree.InorderTraversal())
            {
                actResult[i] = elem;
                i++;
            }

            Assert.AreEqual(expresult, actResult);
        }
    }
}
