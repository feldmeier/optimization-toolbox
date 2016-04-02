using CommonTools.Common;

namespace OptimizationBenchmarks
{
    public abstract class Benchmark<T>
    {
        protected readonly int Dimension;
        public abstract void Run(Solution<T> solution);

        protected Benchmark(int dimension)
        {
            this.Dimension = dimension;
        }
    }
}
