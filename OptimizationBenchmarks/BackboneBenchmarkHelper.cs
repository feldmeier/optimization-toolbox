using System;
using CommonTools.Common;

namespace OptimizationBenchmarks
{
    public class BackboneBenchmarkHelper<T> : Benchmark<T>
    {
        private readonly Action<Solution<T>> BackboneBenchmark; 

        public BackboneBenchmarkHelper(Action<Solution<T>> backboneBenchmark, int dimension) : base(dimension)
        {
            this.BackboneBenchmark = backboneBenchmark;
        }

        public override void Run(Solution<T> solution)
        {
            this.BackboneBenchmark(solution);
        }
    }
}
