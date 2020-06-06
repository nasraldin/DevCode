using System;
using System.Collections.Generic;
using System.Linq;

namespace DevCode.Extensions.Collections
{
    /// <summary>
    /// Extension methods for Collections.
    /// </summary>
    public static class CollectionExtensions
    {
        /// <summary>
        /// Checks whatever given collection object is null or has no item.
        /// </summary>
        public static bool IsNullOrEmpty<T>(this ICollection<T> source)
        {
            return source == null || source.Count <= 0;
        }

        /// <summary>
        /// Adds an item to the collection if it's not already in the collection.
        /// </summary>
        /// <param name="source">Collection</param>
        /// <param name="item">Item to check and add</param>
        /// <typeparam name="T">Type of the items in the collection</typeparam>
        /// <returns>Returns True if added, returns False if not.</returns>
        public static bool AddIfNotContains<T>(this ICollection<T> source, T item)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            if (source.Contains(item))
            {
                return false;
            }

            source.Add(item);
            return true;
        }

        /// <summary>
        /// Adds an element to a collection only if a given condition is met. Same as if (some condition) collection.Add(element).
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="predicate"></param>
        /// <param name="item"></param>
        /// <example>collection.AddIf(() => TodayIsFraiday() , collection)</example>
        /// <example>collection.AddIf(p => p.day >= 7, item)</example>
        public static void AddIf<T>(this ICollection<T> collection, Func<bool> predicate, T item)
        {
            if (predicate.Invoke())
                collection.Add(item);
        }

        /// <summary>
        /// Adds an element to a collection only if a given condition is met. Same as if (some condition) collection.Add(element).
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="predicate"></param>
        /// <param name="item"></param>
        /// <example>collection.AddIf(() => TodayIsFraiday() , collection)</example>
        /// <example>collection.AddIf(p => p.day >= 7, item)</example>
        public static void AddIf<T>(this ICollection<T> collection, Func<T, bool> predicate, T item)
        {
            if (predicate.Invoke(item))
                collection.Add(item);
        }

        /// <summary>
        /// Allows to call RemoveAll() method with the same signature like on List
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="predicate"></param>
        public static void RemoveAll<T>(this ICollection<T> @this, Func<T, bool> predicate)
        {
            if (@this is List<T> list)
            {
                list.RemoveAll(new Predicate<T>(predicate));
            }
            else
            {
                List<T> itemsToDelete = @this.Where(predicate).ToList();

                foreach (var item in itemsToDelete)
                {
                    @this.Remove(item);
                }
            }
        }


        public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> items)
        {
            items.ForEach(item => collection.Add(item));
        }
    }
}