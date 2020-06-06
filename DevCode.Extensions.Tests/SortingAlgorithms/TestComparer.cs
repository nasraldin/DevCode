using System.Collections.Generic;

namespace DevCode.Extensions.Tests.SortingAlgorithms
{
    /// <summary>
    /// This internal class is used to help compare two different objects within the 
    /// IEnumerable object
    /// </summary>
    internal class TestComparer : IComparer<int>
    {
        #region IComparer<int> Members

        public int Compare(int x, int y)
        {
            return x.CompareTo(y);
        }

        #endregion
    }
}
