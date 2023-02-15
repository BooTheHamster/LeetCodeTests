using System;
using NUnit.Framework;

namespace LeetCodeTests
{
    /// <summary>
    /// https://leetcode.com/problems/removing-minimum-and-maximum-from-array/
    /// </summary>
    [TestFixture]
    public class Task2091RemovingMinimumAndMaximumFromArray
    {
        [Test]
        public static void Example1()
        {
            var nums = new[] { 2, 10, 7, 5, 4, 1, 8, 6 };
            var expected = 5;
            var actual = MinimumDeletions(nums);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public static void Example2()
        {
            var nums = new[] { 0, -4, 19, 1, 8, -2, -3, 5 };
            var expected = 3;
            var actual = MinimumDeletions(nums);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public static void Example3()
        {
            var nums = new[] { 101 };
            var expected = 1;
            var actual = MinimumDeletions(nums);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public static void Example4()
        {
            var nums = new[] { 101, 102 };
            var expected = 2;
            var actual = MinimumDeletions(nums);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public static void Example5()
        {
            var nums = new[] { 101, 101 };
            var expected = 2;
            var actual = MinimumDeletions(nums);

            Assert.AreEqual(expected, actual);
        }
        
        [Test]
        public static void Example6()
        {
            var nums = new[] { -1,-53,93,-42,37,94,97,82,46,42,-99,56,-76,-66,-67,-13,10,66,85,-28 };
            var expected = 11;
            var actual = MinimumDeletions(nums);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public static void Example7()
        {
            var nums = new[] { 48,-49,-67,18,-59,-56,47,-26,-24,-73,-96,27,-2,-45 };
            var expected = 5;
            var actual = MinimumDeletions(nums);

            Assert.AreEqual(expected, actual);
        }
        
        private static int MinimumDeletions(int[] nums)
        {
            var length = nums.Length;
            
            if (length < 3)
            {
                return length;
            }

            var minIndex = 0;
            var maxIndex = 0;
            
            for (var i = 1; i < length; i++)
            {
                if (nums[i] < nums[minIndex])
                {
                    minIndex = i;
                }

                if (nums[i] > nums[maxIndex])
                {
                    maxIndex = i;
                }
            }

            var delForMinRight = length - minIndex;
            var delForMinLeft = minIndex + 1;
            var delForMaxRight = length - maxIndex;
            var delForMaxLeft = maxIndex + 1;

            var deletionCount = Math.Max(delForMinLeft, delForMaxLeft);
            deletionCount = Math.Min(deletionCount, Math.Max(delForMinRight, delForMaxRight));
            deletionCount = Math.Min(deletionCount, Math.Min(delForMinLeft, delForMinRight) + Math.Min(delForMaxLeft, delForMaxRight)); 

            return deletionCount;
        }
    }
}
