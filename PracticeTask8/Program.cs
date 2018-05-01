using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeTask8
{
    class Program
    {
        // Task: find a clique of K size in a given graph.
        // Input: the list of vertexes, the list of lines and the size of a clique.
        // Student: Alexey Subbotin. Group: SE-17-1.
        static void Main(string[] args)
        {
            // Getting the list of vertexes.
            string vertexesInput = Console.ReadLine();
            string[] vertexes = vertexesInput.Split(' ');

            // Getting the list of lines.
            string linesInput = Console.ReadLine();
            string[] lines = linesInput.Split(' ');

            // Getting the size of a clique.
            int K = Convert.ToInt32(Console.ReadLine());

            // It's impossible to look for a clique with the size greater than the number of vertexes.
            if (K <= vertexes.Length)
            {
                // The number of lines in a clique.
                int numOfLines = K * (K - 1) / 2;

                // Start of recursion.
                GetCombinations(lines, 0, lines.Length - K, "", K, numOfLines);

                if (allCliques == "")
                    Console.WriteLine("There are no cliques!");
                else
                {
                    // Printing all the found cliques.
                    Console.WriteLine("THE LIST OF CLIQUES:");
                    for (int i = 0; i < allCliques.Length; i++)
                    {
                        if (allCliques[i] != ' ')
                            Console.Write(allCliques[i]);
                        else
                            Console.WriteLine();
                    }
                }
            }
            else
            {
                Console.WriteLine("Impossible!");
            }

            Console.ReadLine();
        }

        // Stores all the found cliques.
        public static string allCliques = "";

        // Function to go through all the possible combinations of lines.
        // In order to optimise the search it goes like this (example for K=3 and 3 lines):
        // 0 1
        // 1 2
        // 2 3
        // Combinations will be: 012 013 123.
        public static string GetCombinations(string[] lines, int startIndex, int endIndex, string combination, int K, int numOfLines)
        {
            // Condition to go out of the recursion.
            if (numOfLines == 0)
            {
                // Analysing the created string of lines.
                Analysis(combination, K);
                return "";
            }
            else
            {
                // if the endIndex went out of index range.
                if (endIndex >= lines.Length)
                    endIndex = lines.Length - 1;

                // Going through all the lines from startIndex to endIndex.
                for (int i = startIndex; i <= endIndex; i++)
                {
                    // Adding the line.
                    combination += lines[i];

                    // Going to the next tier.
                    combination += GetCombinations(lines, i + 1, endIndex + 1, combination, K, numOfLines - 1);

                    // After working with the next tier, the current line should be replaced with the next one.
                    combination = combination.Substring(0, combination.Length - 2);

                }

                return "";
            }
        }

        // Function to analyse the created list of lines.
        // If lines create a clique then the number of different vertexes should be equal.
        // For instance: ab ac bc, there are 2 of each letters.
        // ab dc dc, the numbers of different letters are not equal – not a clique.
        public static void Analysis(string combination, int K)
        {
            // Creating the array of letters (vertexes).
            char[] vertexes = combination.ToCharArray();

            // Sorting it. So we get something like that: aabbcc (a clique).
            Array.Sort(vertexes);

            // Indicates if it's a clique or not.
            bool ok = true;

            // "Jumping" from letter to letter assuming it's a clique.
            for (int i = 0; i < vertexes.Length; i += K - 1)
            {
                // Comparing letters in a group.
                for (int j = i; j < i + K - 2; j++)
                {
                    // If some letters are not equal in the group then it doesn't look like aabbcc, then it's not a clique.
                    if (vertexes[j] != vertexes[j + 1])
                    {
                        ok = false;
                        break;
                    }
                }

                if (!ok)
                    break;
            }

            // If it's a clique then store it.
            if (ok)
            {
                for (int i = 0; i < vertexes.Length; i += K - 1)
                {
                    allCliques += vertexes[i];
                }
                allCliques += " ";
            }

        }
    }
}
