using System.Collections.Generic;
using CommonTools.Common;

namespace OptimizationAlgorithms.GeneticAlgorithm.CrossoverSchemes
{
    public abstract class CrossoverScheme<T>
    {
        public abstract Solution<T> Crossover(Solution<T> sol1, Solution<T> sol2);
    }
}
