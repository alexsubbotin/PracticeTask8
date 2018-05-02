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
            string[] vertexes = VertexGenerator();
            foreach (string v in vertexes)
                Console.Write(v + " ");
            Console.WriteLine();

            //string vertexesInput = Console.ReadLine();
            //string[] vertexes = vertexesInput.Split(' ');

            // Getting the list of lines.
            string[] lines = LinesGenerator(vertexes);
            foreach (string l in lines)
                Console.Write(l + " ");
            Console.WriteLine();

            //string linesInput = Console.ReadLine();
            //string[] lines = linesInput.Split(' ');

            // Getting the size of a clique (K > 2).
            int K = GetK();

            // It's impossible to look for a clique with the size greater than the number of vertexes.
            if (K <= vertexes.Length)
            {
                // The number of lines in a clique.
                int numOfLines = K * (K - 1) / 2;

                int endIndex;
                if (K == 2)
                    endIndex = lines.Length - 1;
                else
                    endIndex = lines.Length - K;

                // Start of recursion.
                GetCombinations(lines, 0, endIndex, "", K, numOfLines);

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

        // Method to get K.
        public static int GetK()
        {
            int K;
            bool ok;
            do
            {
                Console.Write("Enter the K: ");
                ok = Int32.TryParse(Console.ReadLine(), out K);
                if (!ok || K < 2)
                    Console.WriteLine("Input error! K shoud be a natural number greater than 1");
            } while (!ok || K < 2);

            return K;
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

        public static Random rnd = new Random();

        // Creating the list of vertexes.
        public static string[] VertexGenerator()
        {
            string[] vertexes = new string[rnd.Next(2, 11)];
            for (int i = 0; i < vertexes.Length; i++)
                vertexes[i] = ((char)(i + 65)).ToString();

            return vertexes;
        }

        // Creating the list of lines.
        public static string[] LinesGenerator(string[] vertexes)
        {
            string linesStr = "";

            for (int i = 0; i < vertexes.Length; i++)
            {
                // Number of lines with a current vertex.
                int numOfCurrLines = rnd.Next(0, vertexes.Length - i - 1);

                // The lisy of lines with a current vertex.
                string currLines = "";

                for (int j = 0; j < numOfCurrLines; j++)
                {
                    // A random index of a second vertex in a line.
                    int randomIndex;

                    do
                    {
                        randomIndex = rnd.Next(i, vertexes.Length - 1);
                    } while (currLines.Contains(vertexes[randomIndex]) || vertexes[randomIndex] == vertexes[i]);

                    currLines += vertexes[i] + vertexes[randomIndex] + " ";
                }

                linesStr += currLines;
            }

            if (linesStr != "")
                linesStr = linesStr.Remove(linesStr.Length - 1, 1);

            string[] lines = linesStr.Split(' ');

            return lines;
        }
    }
}
