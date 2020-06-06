using System;
using System.Collections;

namespace DevCode.Extensions.Collections
{
    public static class ArrayListExtensions
    {
        /// <summary>
        /// Shuffle an ArrayList in O(n) time (fastest possible way in theory and practice!).
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static ArrayList Shuffle(this ArrayList list)
        {
            var r = new Random((int)DateTime.Now.Ticks);
            for (int i = list.Count - 1; i > 0; i--)
            {
                int j = r.Next(0, i - 1);
                var e = list[i];
                list[i] = list[j];
                list[j] = e;
            }
            return list;
        }
    }
}
