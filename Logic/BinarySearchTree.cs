using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class ThreeNode<T>
    {
        public T data;
        public ThreeNode<T> Left;
        public ThreeNode<T> Right;

        public ThreeNode(T value, ThreeNode<T> left=null, ThreeNode<T> right=null)
        {
            data = value;
            Left = left;
            Right = right;
        }
    }
    public class BinarySearchTree<T> : IEnumerable<T>
    {
        private ThreeNode<T> root;
        private Comparer<T> comparer;
        private int count;
        private int order;

        #region cctors
        public BinarySearchTree() 
        {
            if (typeof(T).GetInterfaces().Contains(typeof(IComparable)) || typeof(T).GetInterfaces().Contains(typeof(IComparable<T>))
                || typeof(T).GetInterfaces().Contains(typeof(IComparer)) || typeof(T).GetInterfaces().Contains(typeof(IComparer<T>)))
            {
                TreeBuilder(null, Comparer<T>.Default);
            }
            else throw new ArgumentException($"Type {typeof(T)} don't have default comparer," +
                                                                         $"use another constructor with comparer argument");
        }
        public BinarySearchTree(IEnumerable<T> collection)
        {
            if (typeof(T).GetInterfaces().Contains(typeof(IComparable)) || typeof(T).GetInterfaces().Contains(typeof(IComparable<T>))
                || typeof(T).GetInterfaces().Contains(typeof(IComparer)) || typeof(T).GetInterfaces().Contains(typeof(IComparer<T>)))
            {
                TreeBuilder(collection, Comparer<T>.Default);
            }
            else throw new ArgumentException($"Type {typeof(T)} don't have default comparer," +
                                             $"use another constructor with comparer argument");
        }
        public BinarySearchTree(Comparer<T> comparer)
        {
            if (comparer == null) throw new ArgumentNullException();
            TreeBuilder(null, comparer);
        }
        public BinarySearchTree(IEnumerable<T> collection, Comparer<T> comparer)
        {
            if(comparer==null) throw new ArgumentNullException();
            TreeBuilder(collection,comparer);
        }
        #endregion

        #region PublicMethods
        /// <summary>
        /// Amount of elements in tree
        /// </summary>
        public int Count => count;
        /// <summary>
        /// Maximum depth of tree
        /// </summary>
        public int Depth => DepthForNode(root);
        /// <summary>
        /// Adds element into tree, ignores if already element exists
        /// </summary>
        /// <param name="value">value for adding</param>
        public void Add(T value)
        {
            if (ReferenceEquals(root, null))
            {
                count++;
                root = new ThreeNode<T>(value);
                return;
            }
            ThreeNode<T> tmp = root;
            while (true)
            {
                if (comparer.Compare(value, tmp.data) < 0)
                {
                    if (tmp.Left == null)
                    {
                        count++;
                        tmp.Left = new ThreeNode<T>(value);
                        break;
                    }
                    tmp = tmp.Left;
                }
                else
                {
                    if (tmp.Right == null)
                    {
                        count++;
                        tmp.Right = new ThreeNode<T>(value);
                        break;
                    }
                    tmp = tmp.Right;
                }
            }
        }
        /// <summary>
        /// Iterative preorder traversal implementation
        /// </summary>
        /// <returns>enumeration</returns> 
        public IEnumerator<T> GetEnumerator()
        {
            if (root == null) yield break;
            Stack<ThreeNode<T>> stack = new Stack<ThreeNode<T>>(Depth);
            stack.Push(root);
            while (stack.Count != 0)
            {
                ThreeNode<T> tmp = stack.Peek();
                yield return stack.Pop().data;
                if (tmp.Right != null) stack.Push(tmp.Right);
                if (tmp.Left != null) stack.Push(tmp.Left);
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        public IEnumerable<T> PreorderTraversal() => PreorderRecursion(root);
        public IEnumerable<T> InorderTraversal() => InorderRecursion(root);
        public IEnumerable<T> PostorderTraversal() => PostorderRecursion(root);
        #endregion

        #region PrivateMethods
        private void TreeBuilder(IEnumerable<T> collection, Comparer<T> comparer)
        {
            this.comparer = comparer;
            count = 0;
            if (collection != null)
            {
                foreach (T element in collection)
                {
                    Add(element);
                }
            }
        }
        private int DepthForNode(ThreeNode<T> node)
        {
            if (node == null) return 0;
            return Math.Max(DepthForNode(node.Left) + 1, DepthForNode(node.Right) + 1);
        }
        private IEnumerable<T> PostorderRecursion(ThreeNode<T> node)
        {
            if (node == null) yield break;

            IEnumerable<T> left = PostorderRecursion(node.Left);
            foreach (T element in left)
            {
                yield return element;
            }

            IEnumerable<T> right = PostorderRecursion(node.Right);
            foreach (T element in right)
            {
                yield return element;
            }

            yield return node.data;
        }
        private IEnumerable<T> PreorderRecursion(ThreeNode<T> node)
        {
            if (node == null) yield break;

            yield return node.data;

            IEnumerable<T> left = PreorderRecursion(node.Left);
            foreach (T element in left)
            {
                yield return element;
            }

            IEnumerable<T> right = PreorderRecursion(node.Right);
            foreach (T element in right)
            {
                yield return element;
            }
        }
        private IEnumerable<T> InorderRecursion(ThreeNode<T> node)
        {
            if (node == null) yield break;

            IEnumerable<T> left = InorderRecursion(node.Left);
            foreach (T element in left)
            {
                yield return element;
            }

            yield return node.data;

            IEnumerable<T> right = InorderRecursion(node.Right);
            foreach (T element in right)
            {
                yield return element;
            }
        }
        #endregion
    }
}
