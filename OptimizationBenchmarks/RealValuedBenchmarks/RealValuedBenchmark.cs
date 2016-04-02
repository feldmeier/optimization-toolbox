using CommonTools.Common;

namespace OptimizationBenchmarks.RealValuedBenchmarks
{
    public abstract class RealValuedBenchmark : Benchmark<double>
    {
        private static double Epsilon = 10e-2;

        public double[] ComputeNumericalGradient(Solution<double> solution)
        {
            double[] gradient = new double[solution.DecisionVariables.Length];
            Solution<double> tempSol = solution.Copy();

            for (int i = 0; i < gradient.Length; i++)
            {
                tempSol.DecisionVariables[i] += Epsilon;
                this.Run(tempSol);

                gradient[i] = (tempSol.Quality - solution.Quality) / Epsilon;
                tempSol.DecisionVariables[i] = solution.DecisionVariables[i];
            }
            return gradient;
        }

        public abstract double[] ComputeAnalyticalGradient(Solution<double> solution);

        protected RealValuedBenchmark(int dimension) : base(dimension)
        {

        }
    }
}
