using System.Collections.Generic;
using NUnit.Framework;

namespace LeetCodeTests
{
    /// <summary>
    /// https://leetcode.com/problems/two-sum/
    /// </summary>
    [TestFixture]
    public class Task1TwoSum
    {
        [Test]
        public static void Example1()
        {
            var expected = new int[] { 0, 1 };
            var actual = TwoSum(new[] { 2, 7, 11, 15 }, 9);

            Assert.IsNotEmpty(actual);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public static void Example2()
        {
            var expected = new int[] { 1, 2 };
            var actual = TwoSum(new[] { 3, 2, 4 }, 6);

            Assert.IsNotEmpty(actual);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public static void Example3()
        {
            var expected = new int[] { 0, 1 };
            var actual = TwoSum(new[] { 3, 3 }, 6);

            Assert.IsNotEmpty(actual);
            Assert.AreEqual(expected, actual);
        }

        private static int[] TwoSum(int[] nums, int target)
        {
            var len = nums.Length;
            var numSet = new HashSet<int>(nums);

            for (var firstIndex = 0; firstIndex < len; firstIndex++)
            {
                var diff = target - nums[firstIndex];

                if (numSet.Contains(diff))
                {
                    for (var secondIndex = 0; secondIndex < len; secondIndex++)
                    {
                        if (firstIndex == secondIndex)
                        {
                            continue;
                        }

                        if (nums[secondIndex] == diff)
                        {
                            return firstIndex > secondIndex 
                                ? new[] { secondIndex, firstIndex }
                                : new[] { firstIndex, secondIndex };
                        }
                    }
                }
            }

            return null;
        }
    }
}
