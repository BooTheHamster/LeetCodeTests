using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using NUnit.Framework;

namespace LeetCodeTests;

/// <summary>
/// https://leetcode.com/problems/find-all-people-with-secret/description/
/// </summary>
[TestFixture]
public class Task2092FindAllPeopleWithSecret
{
    private static readonly JsonSerializerOptions Options = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase 
    };
    
    [Test]
    public static void Example1()
    {
        const int n = 6;
        var meetings = new[]
        {
            new[] {1, 2, 5},
            new[] {2, 3, 8},
            new[] {1, 5, 10}
        };
        const int firstPerson = 1;

        var expected = new List<int> { 0, 1, 2, 3, 5 };
            
        var solver = new Solution();
        var actual = solver.FindAllPeople(n, meetings, firstPerson);

        AssertAreEqual(expected, actual);
    }

    [Test]
    public static void Example2()
    {
        const int n = 4;
        var meetings = new[]
        {
            new[] {3, 1, 3},
            new[] {1, 2, 2},
            new[] {0, 3, 3}
        };
        const int firstPerson = 3;

        var expected = new List<int> { 0, 1, 3 };
            
        var solver = new Solution();
        var actual = solver.FindAllPeople(n, meetings, firstPerson);

        AssertAreEqual(expected, actual);
    }

    [Test]
    public static void Example3()
    {
        const int n = 5;
        var meetings = new[]
        {
            new[] {3, 4, 2},
            new[] {1, 2, 1},
            new[] {2, 3, 1}
        };
        const int firstPerson = 1;

        var expected = new List<int> { 0, 1, 2, 3, 4 };
            
        var solver = new Solution();
        var actual = solver.FindAllPeople(n, meetings, firstPerson);

        AssertAreEqual(expected, actual);
    }

    [Test]
    public static void Example4()
    {
        const int n = 11;
        var meetings = new[]
        {
            new[] {5, 1, 4},
            new[] {0, 4, 18}
        };
        const int firstPerson = 1;

        var expected = new List<int> { 0, 1, 4, 5 };
            
        var solver = new Solution();
        var actual = solver.FindAllPeople(n, meetings, firstPerson);

        AssertAreEqual(expected, actual);
    }

    [Test]
    public static void ExampleOneHundred()
    {
        const int n = 100000;
        var meetings = new int[n][];
        var expected = new List<int>(n);

        for (var i = 0; i < n; i++)
        {
            meetings[i] = new[] { i, 0, 1 };
            expected.Add(i);
        }
        
        const int firstPerson = 1;

        var solver = new Solution();
        var actual = solver.FindAllPeople(n, meetings, firstPerson);

        AssertAreEqual(expected, actual);
    }
    
    [Test]
    public static void Example6()
    {
        const int n = 6;
        var meetings = new[]
        {
            new[] {4, 5, 1},
            new[] {0, 2, 1},
            new[] {1, 3, 1}
        };
        const int firstPerson = 1;

        var expected = new List<int> { 0, 1, 2, 3 };
            
        var solver = new Solution();
        var actual = solver.FindAllPeople(n, meetings, firstPerson);

        AssertAreEqual(expected, actual);
    }

    [Test]
    public static void Example7()
    {
        const int n = 6;
        var meetings = new[]
        {
            new[] {4, 5, 1},
            new[] {1, 3, 1},
            new[] {0, 4, 1}
        };
        const int firstPerson = 1;

        var expected = new List<int> { 0, 1, 3, 4, 5 };
            
        var solver = new Solution();
        var actual = solver.FindAllPeople(n, meetings, firstPerson);

        AssertAreEqual(expected, actual);
    }

    [Test]
    public static void Example7336()
    {
        var testCaseData = ReadTestCaseDataFromFile("Task2092_case_7336.json");
        
        var solver = new Solution();
        var actual = solver.FindAllPeople(testCaseData.N, testCaseData.Meetings, testCaseData.FirstPerson);

        AssertAreEqual(testCaseData.Expected, actual);
    }

    private static void DebugPrint(ICollection<int> expected, ICollection<int> actual)
    {
        if (expected.Count != actual.Count)
        {
            Console.WriteLine($"{expected.Count} {actual.Count}");
        }

        var actualSet = new HashSet<int>(actual);

        foreach (var i in expected)
        {
            if (!actualSet.Contains(i))
            {
                Console.WriteLine($"Not found: {i}");
            }
        }
    }
    
    private static void AssertAreEqual(ICollection<int> expected, ICollection<int> actual)
    {
        Assert.AreEqual(expected.Count, actual.Count);

        var actualSet = new HashSet<int>(actual);

        foreach (var expectedValue in expected)
        {
            if (!actualSet.Contains(expectedValue))
            {
                Assert.Fail();
            }
        }
    }

    private static TestCaseData ReadTestCaseDataFromFile(string pathAndFilename)
    {
        var jsonData = File.ReadAllText(pathAndFilename);
        var data = JsonSerializer.Deserialize<TestCaseData>(jsonData, Options);

        return data;
    }
    
    public class TestCaseData
    {
        public int N { get; set; }
        public int[][] Meetings { get; set; }
        public int FirstPerson { get; set; }
        public int[] Expected { get; set; }
    }
    
    // ReSharper disable once MemberCanBePrivate.Global
    public class Solution
    {
        private static Queue<int[]> Process(Queue<int[]> queue, bool[] shared)
        {
            var result = new Queue<int[]>();
            
            do
            {
                var meeting = queue.Dequeue();
                var person1 = meeting[0];  
                var person2 = meeting[1];  
                    
                if (shared[person1])
                {
                    shared[person2] = true;
                    continue;
                }

                if (shared[person2])
                {
                    shared[person1] = true;
                    continue;
                }
                    
                result.Enqueue(meeting);

            } while (queue.Count > 0);

            return result;
        }

        public IList<int> FindAllPeople(int n, int[][] meetings, int firstPerson)
        {
            var shared = new bool[n];
            
            // Zero and first person always share a secret.
            shared[0] = true;
            shared[firstPerson] = true;

            Array.Sort(meetings, (left, right) => left[2].CompareTo(right[2]));

            var queue = new Queue<int[]>();

            for (int i = 0, max = meetings.Length; i < max;)
            {
                var time = meetings[i][2];

                for (;i < max && meetings[i][2] == time; ++i)
                {
                    queue.Enqueue(meetings[i]);
                }
                
                if (queue.Count > 0)
                {
                    int count;
                    do
                    {
                        count = queue.Count;
                        var newQueue = Process(queue, shared);

                        if (newQueue.Count == 0)
                        {
                            break;
                        }

                        queue = newQueue;
                    } while (queue.Count < count);
                    
                    queue.Clear();
                }
            }

            var result = new List<int>();

            for (int i = 0, max = shared.Length; i < max; i++)
            {
                if (shared[i])
                {
                    result.Add(i);
                }
            }

            return result;
        }
    }
}