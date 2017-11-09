using System;
using System.Collections.Generic;
using System.Linq;

namespace Braces
{
    class Program
    {
        static string[] test = new string[] { "{[}]}", "{}[{}]([])", "{{[]", "[{((()))}]", "{({()}}"};
        static string[] testAssert = new string[] { "NO", "YES", "NO", "YES", "NO" };

        static void Main(string[] args)
        {
            string[] results = braces(test);

            for (int i = 0; i < results.Length; i++)
            {
                Console.WriteLine($"Test {i + 1}: " + ((results[i] == testAssert[i]) ? "SUCCEEDED" : "FAILED"));
            }
            Console.ReadKey();
        }

        static string[] braces(string[] values)
        {
            Dictionary<char, char> bracePairs = new Dictionary<char, char>();
            bracePairs['}'] = '{';
            bracePairs[']'] = '[';
            bracePairs[')'] = '(';

            List<string> results = new List<string>();

            foreach (string str in values)
            {
                // If openBraces have not all been closed, return NO
                results.Add(AreBracesBalanced(bracePairs, str) ? "YES" : "NO");
            }

            return results.ToArray();
        }

        private static bool AreBracesBalanced(Dictionary<char, char> bracePairs, string str)
        {
            Stack<char> openBraces = new Stack<char>();

            foreach (char c in str)
            {
                // If this is a closing brace
                if (bracePairs.ContainsKey(c))
                {
                    // Remove the current top level brace if it's a match
                    if (bracePairs[c] == openBraces.Peek())
                    {
                        openBraces.Pop();
                    }
                    else
                    {
                        // Closing brace with no openning brace automatically fails the test
                        return false;
                    }
                }
                else
                {
                    openBraces.Push(c);
                }
            }

            return !openBraces.Any();
        }
    }
}
