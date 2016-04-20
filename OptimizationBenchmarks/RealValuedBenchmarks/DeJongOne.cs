using System;
using CommonTools.Common;

namespace OptimizationBenchmarks.RealValuedBenchmarks
{
    public class DeJongOne : RealValuedBenchmark
    {
        public DeJongOne(int dimension) : base(dimension)
        {
            
        }

        public override void Run(Solution<double> solution)
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
                    result += Math.Pow(solution.DecisionVariables[i], 2.0);
                }
                solution.Quality = result;
            }
        }

        public override double[] ComputeAnalyticalGradient(Solution<double> solution)
        {
            double[] gradient = new double[solution.DecisionVariables.Length];
            for (int i = 0; i < gradient.Length; i++)
            {
                gradient[i] = 2*solution.DecisionVariables[i];
            }
            return gradient;
        }
    }
}
