using System;

namespace CommonTools.Common
{
    public class RealValuedSolution : Solution<double>
    {
        public RealValuedSolution(int dimension) : base(dimension)
        {
        }

        public RealValuedSolution(Solution<double> solution) : base(solution.Dimension)
        {
            this.DecisionVariables = solution.DecisionVariables;
            this.Quality = solution.Quality;
        }

        public double EuclideanDistance(RealValuedSolution solution)
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

        public override Solution<string> toStringSolution()
        {
            Solution<string> res = new Solution<string>(this.Dimension);

            for (int i = 0; i < this.Dimension; i++)
            {
                res.DecisionVariables[i] = string.Format("{0:0.000}", this.DecisionVariables[i]);
            }

            return res;
        }

        public static RealValuedSolution operator +(RealValuedSolution s1, RealValuedSolution s2)
        {
            RealValuedSolution res = new RealValuedSolution(s1.DecisionVariables.Length);
            res.Quality = double.NaN;
            for (int i = 0; i < s1.DecisionVariables.Length; i++)
            {
                res.DecisionVariables[i] = s1.DecisionVariables[i] + s2.DecisionVariables[i];
            }
            return res;
        }

        public static RealValuedSolution operator *(RealValuedSolution s1, double scalar)
        {
            RealValuedSolution res = new RealValuedSolution(s1.DecisionVariables.Length);
            res.Quality = double.NaN;
            for (int i = 0; i < s1.DecisionVariables.Length; i++)
            {
                res.DecisionVariables[i] = s1.DecisionVariables[i] * scalar;
            }
            return res;
        }

        public static RealValuedSolution operator -(RealValuedSolution s1, RealValuedSolution s2)
        {
            return s1 + (s2*-1);
        }

        public static RealValuedSolution operator /(RealValuedSolution s1, double scalar)
        {
            return s1*(1/scalar);
        }
    }
}
