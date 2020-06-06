using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace DevCode.Extensions.Collections
{
    /// <summary>
    /// Extension methods for <see cref="IList{T}"/>.
    /// </summary>
    public static class ListExtensions
    {
        /// <summary>
        /// Sort a list by a topological sorting, which consider their  dependencies
        /// </summary>
        /// <typeparam name="T">The type of the members of values.</typeparam>
        /// <param name="source">A list of objects to sort</param>
        /// <param name="getDependencies">Function to resolve the dependencies</param>
        /// <returns></returns>
        public static List<T> SortByDependencies<T>(this IEnumerable<T> source, Func<T, IEnumerable<T>> getDependencies)
        {
            /* See: http://www.codeproject.com/Articles/869059/Topological-sorting-in-Csharp
             *      http://en.wikipedia.org/wiki/Topological_sorting
             */

            var sorted = new List<T>();
            var visited = new Dictionary<T, bool>();

            foreach (var item in source)
            {
                SortByDependenciesVisit(item, getDependencies, sorted, visited);
            }

            return sorted;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">The type of the members of values.</typeparam>
        /// <param name="item">Item to resolve</param>
        /// <param name="getDependencies">Function to resolve the dependencies</param>
        /// <param name="sorted">List with the sortet items</param>
        /// <param name="visited">Dictionary with the visited items</param>
        private static void SortByDependenciesVisit<T>(T item, Func<T, IEnumerable<T>> getDependencies, List<T> sorted, Dictionary<T, bool> visited)
        {
            var alreadyVisited = visited.TryGetValue(item, out bool inProcess);

            if (alreadyVisited)
            {
                if (inProcess)
                {
                    throw new ArgumentException("Cyclic dependency found! Item: " + item);
                }
            }
            else
            {
                visited[item] = true;

                var dependencies = getDependencies(item);
                if (dependencies != null)
                {
                    foreach (var dependency in dependencies)
                    {
                        SortByDependenciesVisit(dependency, getDependencies, sorted, visited);
                    }
                }

                visited[item] = false;
                sorted.Add(item);
            }
        }

        /// <summary>
        /// Insert an item to a sorted List
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <example>l.InsertSorted(3)</example>
        public static int InsertSorted<T>(this IList<T> source, T value) where T : IComparable<T>
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            for (int i = 0; i < source.Count; i++)
            {
                if (value.CompareTo(source[i]) < 0)
                {
                    source.Insert(i, value);
                    return i;
                }
            }
            source.Add(value);
            return source.Count - 1;
        }

        /// <summary>
        /// Insert an item to a sorted List with Comparer
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="value"></param>
        /// <param name="comparison"></param>
        /// <returns></returns>
        public static int InsertSorted<T>(this IList<T> source, T value, IComparer<T> comparison)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            for (int i = 0; i < source.Count; i++)
            {
                if (comparison.Compare(value, source[i]) < 0)
                {
                    source.Insert(i, value);
                    return i;
                }
            }
            source.Add(value);
            return source.Count - 1;
        }

        /// <summary>
        /// Insert an item to a sorted List with CompareTo
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="value"></param>
        /// <param name="comparison"></param>
        /// <returns></returns>
        /// l.InsertSorted(7, (x, y) => x.CompareTo(y))
        public static int InsertSorted<T>(this IList<T> source, T value, Comparison<T> comparison)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            for (int i = 0; i < source.Count; i++)
            {
                if (comparison(value, source[i]) < 0)
                {
                    source.Insert(i, value);
                    return i;
                }
            }
            source.Add(value);
            return source.Count - 1;
        }

        /// <summary>
        /// Allows you to chain .Add method typesafe
        /// </summary>
        /// <typeparam name="TList"></typeparam>
        /// <typeparam name="TItem"></typeparam>
        /// <param name="list"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public static TList Push<TList, TItem>(this TList list, TItem item) where TList : IList<TItem>
        {
            list.Add(item);
            return list;
        }

        /// <summary>
        /// Determines whether the given IList object [is null or empty].
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns><c>true</c> if the given IList object [is null or empty]; otherwise, <c>false</c>.</returns>
        public static bool IsNullOrEmpty<T>(this IList<T> obj)
        {
            return obj == null || obj.Count == 0;
        }

        /// <summary>
        /// lambda expression to replace the first item that satisfies the condition.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="Tkey"></typeparam>
        /// <param name="source"></param>
        /// <param name="replacement"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static IEnumerable<TSource> Replace<TSource, Tkey>(this IList<TSource> source, TSource replacement, Func<TSource, Tkey> selector)
        {
            foreach (var item in source)
            {
                var key = selector(item);

                if (key.Equals(true))
                {
                    int index = source.IndexOf(item);
                    source.Remove(item);
                    source.Insert(index, replacement);
                    break;
                }
            }
            return source;
        }

        /// <summary>
        /// Shuffle any (I)List with an extension method based on the Fisher-Yates shuffle.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        public static void Shuffle<T>(this IList<T> list)
        {
            RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
            int n = list.Count;
            while (n > 1)
            {
                byte[] box = new byte[1];
                do provider.GetBytes(box);
                while (!(box[0] < n * (Byte.MaxValue / n)));
                int k = (box[0] % n);
                n--;
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        /// <summary>
        /// Allows to clone an etire generic list of cloneable items.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="listToClone"></param>
        /// <returns></returns>
        public static IList<T> Clone<T>(this IList<T> listToClone) where T : ICloneable
        {
            return listToClone.Select(item => (T)item.Clone()).ToList();
        }
    }
}
