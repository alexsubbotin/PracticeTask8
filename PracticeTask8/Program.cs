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

            // The number of lines in a full graph equal k*(k-1)/2.
            int numberOfLines = K * (K - 1) / 2;
        }

        public static string GetCombination(string[] lines, int numberOfLines, int startIndex)
        {

        }
    }
}
