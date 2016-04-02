using System;
using CommonTools.Common;

namespace OptimizationBenchmarks.RealValuedBenchmarks
{
    public class DeJongFour : RealValuedBenchmark
    {
        public DeJongFour(int dimension) : base(dimension)
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
                    result += (i+1)*Math.Pow(solution.DecisionVariables[i], 4.0);
                }
                solution.Quality = result;
            }
        }

        public override double[] ComputeAnalyticalGradient(Solution<double> solution)
        {
            double[] gradient = new double[solution.DecisionVariables.Length];
            for (int i = 0; i < gradient.Length; i++)
            {
                gradient[i] = (i+1)*4*Math.Pow(solution.DecisionVariables[i], 3);
            }
            return gradient;
        }
    }
}
