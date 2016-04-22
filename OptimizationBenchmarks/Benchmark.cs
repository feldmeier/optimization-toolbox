using CommonTools.Common;

namespace OptimizationBenchmarks
{
    public abstract class Benchmark<T>
    {
        protected readonly int Dimension;
        public abstract void Evaluate(Solution<T> solution);

        protected Benchmark(int dimension)
        {
            this.Dimension = dimension;
        }

        public void EvaluateAll(SolutionSet<T> solutionSet)
        {
            foreach(Solution<T> solution in solutionSet.Solutions)
            {
                this.Evaluate(solution);
            }
        }
    }
}
