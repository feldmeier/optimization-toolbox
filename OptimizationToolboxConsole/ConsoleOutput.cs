using System;
using CommonTools.Common;

namespace OptimizationToolboxConsole
{
    public static class ConsoleOutput
    {
        public static void PrintSolutionWithQuality<T>(Solution<T> solution)
        {
            Solution<string> display = solution.toStringSolution();
            Console.Write("{0:0.000} | ", solution.Quality);
            for (int i = 0; i < display.DecisionVariables.Length; i++)
            {
                Console.Write("{0} ", display.DecisionVariables[i]);
            }
            Console.WriteLine();
        }

        public static void PrintBackbones(bool[] bbs)
        {
            for (int i = 0; i < bbs.Length; i++)
            {
                Console.Write("{0}", bbs[i] ? "#" : " ");
            }
            Console.WriteLine();
        }

    }
}
