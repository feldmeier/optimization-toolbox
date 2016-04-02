using System;
using CommonTools.Common;

namespace OptimizationBenchmarks.RealValuedBenchmarks
{
    public class Michalewicz : RealValuedBenchmark
    {
        public Michalewicz(int dimension) : base(dimension)
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
                for (int i = 0; i < this.Dimension; ++i)
                {
                    double a = Math.Sin(solution.DecisionVariables[i]);
                    double b = Math.Sin(((i + 1) * solution.DecisionVariables[i] * solution.DecisionVariables[i]) / Math.PI);
                    double c = Math.Pow(b, 20);
                    result += a * c;
                }
                solution.Quality = result;
            }
        }

        public override double[] ComputeAnalyticalGradient(Solution<double> solution)
        {
            throw new NotImplementedException();
        }
    }
}
