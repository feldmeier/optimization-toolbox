using System;
using CommonTools.Common;

namespace OptimizationBenchmarks.RealValuedBenchmarks
{
    public class DeJongThree : RealValuedBenchmark
    {
        public DeJongThree(int dimension) : base(dimension)
        {

        }

        public override void Evaluate(Solution<double> solution)
        {
            if (solution.DecisionVariables.Length != this.Dimension)
            {
                throw new ArgumentException("Dimension does not fit for this benchmark instance", "solution");
            }
            else
            {
                double result = 0.0;
                for (int i = 0; i < this.Dimension; i++)
                {
                    result += Math.Abs(solution.DecisionVariables[i]);
                }
                solution.Quality = result;
            }
        }

        public override double[] ComputeAnalyticalGradient(Solution<double> solution)
        {
            double[] gradient = new double[solution.DecisionVariables.Length];
            for (int i = 0; i < gradient.Length; i++)
            {
                gradient[i] = Math.Sign(solution.DecisionVariables[i]);
            }
            return gradient;
        }
    }
}
