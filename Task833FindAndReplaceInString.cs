using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace LeetCodeTests
{
    /// <summary>
    /// https://leetcode.com/problems/find-and-replace-in-string/
    /// </summary>
    [TestFixture]
    public class Task833FindAndReplaceInString
    {
        [Test]
        public static void Example1()
        {
            var expected = "eeebffff";
            var actual = FindReplaceString("abcd", new[] { 0, 2 }, new[] { "a", "cd" }, new[] { "eee", "ffff" });

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public static void Example2()
        {
            var expected = "eeecd";
            var actual = FindReplaceString("abcd", new[] { 0, 2 }, new[] { "ab", "ec" }, new[] { "eee", "ffff" });

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public static void Example3()
        {
            var expected = "ffffeeeb";
            var actual = FindReplaceString("cdab", new[] { 2, 0 }, new[] { "a", "cd" }, new[] { "eee", "ffff" });

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public static void Example4()
        {
            var expected = "cdab";
            var actual = FindReplaceString("cdab", new[] { 2, 0 }, new[] { "x", "yy" }, new[] { "eee", "ffff" });

            Assert.AreEqual(expected, actual);
        }

        private static string FindReplaceString(string s, int[] indices, string[] sources, string[] targets)
        {
            var replacements = new List<int[]>();

            for (var i = 0; i < indices.Length; i++)
            {
                var pos = indices[i];
                var len = sources[i].Length;
                
                if (s.Substring(pos, len) == sources[i])
                {
                    replacements.Add(new int[] { pos, len, i });
                }
            }

            if (replacements.Count == 0)
            {
                return s;
            }

            var builder = new StringBuilder();
            var sourcePos = 0;

            foreach (var replacement in replacements.OrderBy(r => r[0]))
            {
                var len = replacement[0] - sourcePos;

                if (len > 0)
                {
                    builder.Append(s.Substring(sourcePos, len));
                    sourcePos += len;
                }

                builder.Append(targets[replacement[2]]);

                sourcePos += replacement[1];
            }

            builder.Append(s.Substring(sourcePos));

            return builder.ToString();
        }
    }
}
