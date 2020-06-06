using DevCode.Extensions.SortingAlgorithms;
using System.Collections.Generic;
using Xunit;

namespace DevCode.Extensions.Tests.SortingAlgorithms
{
    /// <summary>
    /// This is a test class for SortClassTest and is intended to contain all SortClassTest Unit Tests
    ///</summary>
    public class SortClassTest
    {
        /// <summary>
        /// This method is fo testing the bubble algorithm
        /// </summary>
        [Fact]
        public void SortBubbleTest()
        {
            IEnumerable<int> actual = new int[] { 5, 3, 4, 1 };
            IEnumerable<int> result = actual.SortBubble(new TestComparer(), SortOrder.Ascending);

            List<int> actualList = new List<int>(result);

            IEnumerable<int> exprected = new int[] { 1, 3, 4, 5 };
            List<int> expectedList = new List<int>(exprected);

            Assert.Equal(expectedList, actualList);
        }

        /// <summary>
        /// This method is fo testing the sort cocktail algorithm
        /// </summary>
        [Fact]
        public void SortCocktailTest()
        {
            IEnumerable<int> actual = new int[] { 5, 3, 4, 1 };
            IEnumerable<int> result = actual.SortCocktail(new TestComparer(), SortOrder.Ascending);

            List<int> actualList = new List<int>(result);

            IEnumerable<int> exprected = new int[] { 1, 3, 4, 5 };
            List<int> expectedList = new List<int>(exprected);

            Assert.Equal(expectedList, actualList);
        }

        /// <summary>
        /// This method is fo testing the even odd algorithm
        /// </summary>
        [Fact]
        public void SortEvenOddTest()
        {
            IEnumerable<int> actual = new int[] { 5, 3, 4, 1 };
            IEnumerable<int> result = actual.SortEvenOdd(new TestComparer(), SortOrder.Ascending);

            List<int> actualList = new List<int>(result);

            IEnumerable<int> exprected = new int[] { 1, 3, 4, 5 };
            List<int> expectedList = new List<int>(exprected);

            Assert.Equal(expectedList, actualList);
        }

        /// <summary>
        /// This method is fo testing the sort comb algorithm
        /// </summary>
        [Fact]
        public void SortCombTest()
        {
            IEnumerable<int> actual = new int[] { 5, 3, 4, 1 };
            IEnumerable<int> result = actual.SortComb(new TestComparer(), SortOrder.Ascending);

            List<int> actualList = new List<int>(result);

            IEnumerable<int> exprected = new int[] { 1, 3, 4, 5 };
            List<int> expectedList = new List<int>(exprected);

            Assert.Equal(expectedList, actualList);
        }

        /// <summary>
        /// This method is fo testing the sort genome algorithm
        /// </summary>
        [Fact]
        public void SortGenomeTest()
        {
            IEnumerable<int> actual = new int[] { 5, 3, 4, 1 };
            IEnumerable<int> result = actual.SortGenome(new TestComparer(), SortOrder.Descending);

            List<int> actualList = new List<int>(result);

            IEnumerable<int> exprected = new int[] { 5, 4, 3, 1 };
            List<int> expectedList = new List<int>(exprected);

            Assert.Equal(expectedList, actualList);
        }

        /// <summary>
        /// This method is fo testing the sort selection algorithm
        /// </summary>
        [Fact]
        public void SortSelectionTest()
        {
            int[] array = new int[] { 5, 3, 4, 1 };
            IEnumerable<int> actual = array;
            IEnumerable<int> result = actual.SortSelection(new TestComparer(), SortOrder.Descending);

            List<int> actualList = new List<int>(result);

            IEnumerable<int> exprected = new int[] { 5, 4, 3, 1 };
            List<int> expectedList = new List<int>(exprected);

            Assert.Equal(expectedList, actualList);
        }

        /// <summary>
        /// This method is fo testing the sort heap algorithm
        /// </summary>
        [Fact]
        public void SortHeapTest()
        {
            int[] array = new int[] { 2, 1, 5, 4 };
            IEnumerable<int> actual = array;
            IEnumerable<int> result = actual.SortHeap(new TestComparer(), SortOrder.Descending);

            List<int> actualList = new List<int>(result);

            IEnumerable<int> exprected = new int[] { 5, 4, 2, 1 };
            List<int> expectedList = new List<int>(exprected);

            Assert.Equal(expectedList, actualList);
        }
    }
}