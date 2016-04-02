using CommonTools.Common;
using CommonTools.Common.InitializationSchemes;
using OptimizationBenchmarks.RealValuedBenchmarks;

namespace OptimizationAlgorithms.GradientDescent
{
    public class NumericalGradientDescent
    {
        private readonly RealValuedBenchmark Benchmark;

        private readonly InitializationScheme<double> InitializationScheme;

        private readonly int Dimension;

        public NumericalGradientDescent(RealValuedBenchmark benchmark, InitializationScheme<double> initializationScheme, int dimension)
        {
            this.Benchmark = benchmark;
            this.InitializationScheme = initializationScheme;
            this.Dimension = dimension;
        }

        public Solution<double> Iterate(Solution<double> solution, double alpha)
        {
            double[] gradient = this.Benchmark.ComputeNumericalGradient(solution);
            for (int i = 0; i < gradient.Length; i++)
            {
                solution.DecisionVariables[i] -= gradient[i] * alpha;
            }
            this.Benchmark.Run(solution);
            return solution;
        }

        public Solution<double> Run(Solution<double> solution, int iterations, double alpha)
        {
            solution = solution ?? new Solution<double>(this.Dimension);
            this.InitializationScheme.Initialize(solution);
            this.Benchmark.Run(solution);
            int i = 0;
            do
            {
                solution = this.Iterate(solution, alpha);
            } while (++i < iterations);
            return solution;
        }
    }
}
