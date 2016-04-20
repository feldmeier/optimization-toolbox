using System;
using System.Collections.Generic;
using System.Linq;
using CommonTools.Common;
using CommonTools.Util.RandomNumberGeneration;

namespace OptimizationAlgorithms.GeneticAlgorithm.CrossoverSchemes
{
    public class DoublePointCrossover<T> : CrossoverScheme<T>
    {
        private readonly RandomNumberGenerator Random;

        public DoublePointCrossover(RandomNumberGenerator random)
        {
            this.Random = random;
        }

        public override Solution<T> Crossover(Solution<T> sol1, Solution<T> sol2)
        {
            if (sol1.Dimension != sol2.Dimension)
            {
                throw new ArgumentException("the solutions need to have equal length", "sol1");
            }

            int crossoverPoint1 = this.Random.Next(sol1.Dimension - 1);
            int crossoverPoint2 = this.Random.Next(crossoverPoint1, sol1.Dimension);

            Solution<T> s = sol2.Copy();
            for (int j = 0; j < crossoverPoint1; j++)
            {
                s.DecisionVariables[j] = sol1.DecisionVariables[j];
            }
            for (int j = crossoverPoint1; j < crossoverPoint2; j++)
            {
                s.DecisionVariables[j] = sol2.DecisionVariables[j];
            }
            for (int j = crossoverPoint2; j < sol1.Dimension; j++)
            {
                s.DecisionVariables[j] = sol1.DecisionVariables[j];
            }

            return s;
        }

    }
}
