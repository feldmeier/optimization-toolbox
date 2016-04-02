using System.Collections.Generic;
using CommonTools.Common;

namespace OptimizationAlgorithms.GeneticAlgorithm.SelectionScheme
{
    public abstract class SelectionScheme<T>
    {
        public abstract Solution<T> Select(List<Solution<T>> solutions);
    }
}
