using System.Collections.Generic;
using CommonTools.Common;

namespace OptimizationAlgorithms.GeneticAlgorithm.CrossoverSchemes
{
    public abstract class CrossoverScheme<T>
    {
        public abstract List<Solution<T>> Crossover(List<Solution<T>> solutions);
    }
}
