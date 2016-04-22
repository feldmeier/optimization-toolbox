using System.Collections.Generic;
using CommonTools.Common;

namespace OptimizationAlgorithms.GeneticAlgorithm.SelectionScheme
{
    public abstract class SelectionScheme<T>
    {
        public abstract Solution<T> Select(SolutionSet<T> solutionSet);

        public Solution<T>[] SelectMultiple(SolutionSet<T> solutionSet, int number)
        {
            Solution<T>[] selected = new Solution<T>[number];

            for (int i = 0; i < number; i++)
            {
                selected[i] = this.Select(solutionSet);
            }

            return selected;
        }
    }
}
