using System;

namespace CommonTools.Common
{
    public class IntegerValuedSolution : Solution<Int32>
    {
        public IntegerValuedSolution(int dimension) : base(dimension)
        {
        }

        public IntegerValuedSolution(Solution<int> solution)
            : base(solution.Dimension)
        {
            this.DecisionVariables = solution.DecisionVariables;
            this.Quality = solution.Quality;
        }

        public double EuclideanDistance(IntegerValuedSolution solution)
        {
            if (solution.DecisionVariables.Length != this.DecisionVariables.Length)
            {
                throw new ArgumentException("dimension mismatch", "solution");
            }

            double distance = 0.0;
            for (int i = 0; i < this.DecisionVariables.Length; i++)
            {
                distance += Math.Pow(this.DecisionVariables[i] - solution.DecisionVariables[i], 2);
            }

            return Math.Sqrt(distance);
        }

        public static IntegerValuedSolution operator +(IntegerValuedSolution s1, IntegerValuedSolution s2)
        {
            IntegerValuedSolution res = new IntegerValuedSolution(s1.DecisionVariables.Length);
            res.Quality = double.NaN;
            for (int i = 0; i < s1.DecisionVariables.Length; i++)
            {
                res.DecisionVariables[i] = s1.DecisionVariables[i] + s2.DecisionVariables[i];
            }
            return res;
        }
    }
}
