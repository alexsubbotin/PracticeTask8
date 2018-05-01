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
            string vertexes = Console.ReadLine();

            // Getting the list of lines.
            string linesInput = Console.ReadLine();

            // Creating the array of lines.
            string[] lines = linesInput.Split(' ');

            // Getting the size of a clique.
            int K = Convert.ToInt32(Console.ReadLine());

            if (K <= vertexes.Length)
            {
                GetCombinations(lines, 0, lines.Length - K, "", K);
            }
            else
            {
                Console.WriteLine("Impossible!");
            }

            Console.ReadLine();
        }

        public static string GetCombinations(string[] lines, int startIndex, int endIndex, string combination, int K)
        {
            if(endIndex >= lines.Length)
            {
                Analysis(combination, K);
                return "";
            }
            else
            {
                for(int i = startIndex; i <= endIndex; i++)
                {
                    combination += lines[i];

                    combination += GetCombinations(lines, i + 1, endIndex + 1, combination, K);

                    combination = combination.Substring(0, combination.Length - 2);
                }

                return "";
            }
        }

        public static void Analysis(string combination, int K)
        {
            char[] vertexes = combination.ToCharArray();

            Array.Sort(vertexes);

            bool ok = true;

            for(int i = 0; i < vertexes.Length; i += K - 1)
            {
                for(int j = i; j < i + K - 2; j++)
                {
                    if(vertexes[j] != vertexes[j + 1])
                    {
                        ok = false;
                        break;
                    }
                }

                if (!ok)
                    break;
            }

            if (ok)
            {
                for(int i = 0; i < vertexes.Length; i += K - 1)
                {
                    Console.Write(vertexes[i] + " ");
                }
                Console.WriteLine();
            }
            
        }
    }
}
