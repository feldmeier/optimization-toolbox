using System;
using CommonTools.Common;

namespace OptimizationBenchmarks.RealValuedBenchmarks
{
    public class Schwefel : RealValuedBenchmark
    {
        public Schwefel(int dimension) : base(dimension)
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
                    result += -solution.DecisionVariables[i] * Math.Sin(Math.Sqrt(Math.Abs(solution.DecisionVariables[i]))) + 418.982;
                }
                solution.Quality = result;
            }
        }

        public override double[] ComputeAnalyticalGradient(Solution<double> solution)
        {
            double[] res = new double[solution.DecisionVariables.Length];
            for (int i = 0; i < res.Length; i++)
            {
                double x = solution.DecisionVariables[i];
                res[i] = -Math.Sin(Math.Sqrt(Math.Abs(x))) - Math.Sqrt(Math.Abs(x))*Math.Cos(Math.Sqrt(Math.Abs(x)))/2.0;
            }
            return res;
        }
    }
}
