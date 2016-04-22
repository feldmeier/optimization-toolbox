using CommonTools.Common;
using CommonTools.Common.InitializationSchemes;
using CommonTools.Common.UpdateScheme;
using OptimizationBenchmarks;

namespace OptimizationAlgorithms.RandomWalk
{
    public class RandomWalk<T>
    {
        private readonly UpdateScheme<T> UpdateScheme;
        private readonly Benchmark<T> Benchmark;
        private readonly InitializationScheme<T> InitializationScheme;

        private readonly int Dimension;

        public RandomWalk(UpdateScheme<T> updateScheme, int dimension, Benchmark<T> benchmark, InitializationScheme<T> initializationScheme)
        {
            this.UpdateScheme = updateScheme;
            this.Benchmark = benchmark;
            this.InitializationScheme = initializationScheme;
            this.Dimension = dimension;
        }

        public Solution<T> Iterate(Solution<T> solution)
        {
            Solution<T> result = this.UpdateScheme.Update(solution);
            this.Benchmark.Evaluate(result);
            if (result.Quality < solution.Quality)
            {
                return result;
            }
            else
            {
                return solution;
            }
        }

        public Solution<T> Run(Solution<T> solution, int iterations)
        {
            solution = solution ?? new Solution<T>(this.Dimension);
            this.InitializationScheme.Initialize(solution);
            int i = 0;
            do
            {
                solution = this.Iterate(solution);
            } while (++i < iterations);
            return solution;
        }
    }
}
