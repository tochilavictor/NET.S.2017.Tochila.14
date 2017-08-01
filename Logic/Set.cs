using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    class Set<T> : IEnumerable<T> where T : class
    {
        private List<T>[] buckets;
        private IEqualityComparer<T> eqComparer;
        private int count;
        /// <summary>
        /// set constructor with size mode attribute
        /// </summary>
        /// <param name="mode">0 for rather small set(10 buckets), 1 for medium set (50 buckets), 2 for big 100 buckets set, other for 250 buckets</param>
        public Set(byte mode = 0,IEqualityComparer<T> eqComparer = null)
        {
            if (ReferenceEquals(eqComparer, null)) this.eqComparer = (IEqualityComparer<T>) Comparer<T>.Default;
            else this.eqComparer = eqComparer;
            if (mode == 0) buckets = new List<T>[10];
            else if (mode == 1) buckets = new List<T>[50];
            else if (mode == 2) buckets = new List<T>[100];
            else buckets = new List<T>[250];
        }
        
        public int Count => count;

        #region DestructiveMethods

        /// <summary>
        /// Adds element into set, ignores if element already exist
        /// </summary>
        /// <param name="elem">element</param>
        public void Add(T elem)
        {
            if (Contains(elem)) return;
            if (ReferenceEquals(elem, null)) throw new ArgumentNullException();
            count++;
            buckets[GetBucketIndex(eqComparer.GetHashCode(elem))].Add(elem);
        }

        /// <summary>
        /// removes element from set, ignores attempt to delete element, that not exists in set
        /// </summary>
        /// <param name="elem">element</param>
        public void Remove(T elem)
        {
            if (Contains(elem))
            {
                count--;
                buckets[GetBucketIndex(eqComparer.GetHashCode(elem))].Remove(elem);
            }
        }

        /// <summary>
        /// removes elements using predicate logic
        /// </summary>
        /// <param name="predicate">predicate for deleting</param>
        public void RemoveWhere(Predicate<T> predicate)
        {
            if (predicate == null) throw new ArgumentNullException();
            var removelist = new List<T>();
            foreach (T element in this)
            {
                if (predicate(element)) removelist.Add(element);
            }
            ExceptWith(removelist);
        }

        /// <summary>
        /// adds all elements from collections into set (only 1 time)
        /// </summary>
        /// <param name="collection">collection</param>
        public void UnionWith(IEnumerable<T> collection)
        {
            if (collection == null) throw new ArgumentNullException();
            foreach (T element in collection)
            {
                Add(element);
            }
        }

        /// <summary>
        /// removes all elements from set, that don't exist in collection
        /// </summary>
        /// <param name="collection">collection</param>
        public void IntersectWith(IEnumerable<T> collection)
        {
            if (collection == null) throw new ArgumentNullException();
            var newset = new List<T>();
            foreach (T element in collection)
            {
                if (Contains(element)) newset.Add(element);
            }
            Clear();
            UnionWith(newset);
        }

        /// <summary>
        /// removes range of elements
        /// </summary>
        /// <param name="collection"></param>
        public void ExceptWith(IEnumerable<T> collection)
        {
            if (collection == null) throw new ArgumentNullException();
            foreach (var element in collection)
            {
                if (Contains(element))
                {
                    Remove(element);
                }
            }
        }

        /// <summary>
        /// Union unique elements from set and collection
        /// </summary>
        /// <param name="collection">collection</param>
        public void SymmetricExceptWith(IEnumerable<T> collection)
        {
            if (collection == null) throw new ArgumentNullException();
            var unicueElems = new List<T>();
            foreach (var element in collection)
            {
                if (!Contains(element)) unicueElems.Add(element);
            }
            ExceptWith(collection);
            UnionWith(unicueElems);
        }

        #endregion
        #region NonDestructiveMethods
        /// <summary>
        /// checks if element already exist in set
        /// </summary>
        /// <param name="elem">element</param>
        /// <returns>check result</returns>
        public bool Contains(T elem)
        {
            int index = GetBucketIndex(eqComparer.GetHashCode(elem));
            if (buckets[index] == null) buckets[index] = new List<T>();
            foreach (T element in buckets[index])
            {
                if (elem.Equals(element)) return true;
            }
            return false;
        }

        /// <summary>
        /// checks if set is subset of collection
        /// </summary>
        /// <param name="collection">collection</param>
        /// <returns>check result</returns>
        public bool IsSubsetOf(IEnumerable<T> collection)
        {
            if (collection == null) throw new ArgumentNullException();
            foreach (T element in collection)
            {
                if (!Contains(element)) return false;
            }
            return true;
        }

        /// <summary>
        /// checks if collection is subset of set
        /// </summary>
        /// <param name="collection">collection</param>
        /// <returns>check result</returns>
        public bool IsSupersetOf(IEnumerable<T> collection)
        {
            if (collection == null) throw new ArgumentNullException();
            foreach (T element in collection)
            {
                if (!Contains(element)) return false;
            }
            return true;
        }

        /// <summary>
        /// checks if collections Equal to set,(has same elements)
        /// </summary>
        /// <param name="collection"></param>
        /// <returns>check result</returns>
        public bool Overlaps(IEnumerable<T> collection)
        {
            if (collection == null) throw new ArgumentNullException();
            if (Count != collection.Count()) return false;
            foreach (T element in collection)
            {
                if (!Contains(element)) return false;
            }
            return true;
        }
        #endregion
        #region IEnumerable
        public IEnumerator<T> GetEnumerator()
        {
            foreach (List<T> list in buckets)
            {
                if (list != null)
                {
                    foreach (T elem in list)
                    {
                        yield return elem;
                    }
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
        private int GetBucketIndex(int hashcode)
        {
            return Math.Abs(hashcode % buckets.Length);
        }
        private void Clear()
        {
            List<T>[] tmp = new List<T>[buckets.Length];
            buckets = tmp;
            count = 0;
        }
    }
}
