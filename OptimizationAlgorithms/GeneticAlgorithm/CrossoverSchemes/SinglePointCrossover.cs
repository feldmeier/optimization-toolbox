using System;
using System.Collections.Generic;
using System.Linq;
using CommonTools.Common;
using CommonTools.Util.RandomNumberGeneration;

namespace OptimizationAlgorithms.GeneticAlgorithm.CrossoverSchemes
{
    public class SinglePointCrossover<T> : CrossoverScheme<T>
    {
        private readonly RandomNumberGenerator Random;

        public SinglePointCrossover(RandomNumberGenerator random)
        {
            this.Random = random;
        }

        public override List<Solution<T>> Crossover(List<Solution<T>> solutions)
        {
            Solution<T>[] sol = solutions.ToArray();
            if (solutions == null || sol.Length <= 1)
            {
                throw new ArgumentException("list is null or number of solutions <= 1", "solutions");
            }

            if (sol[0].DecisionVariables.Length != sol[1].DecisionVariables.Length)
            {
                throw new ArgumentException("the solutions need to have equal length", "solutions");
            }

            List<Solution<T>> result = new List<Solution<T>>();
            
            int dimension = sol.FirstOrDefault().DecisionVariables.Length;
            int crossoverPoint = this.Random.Next(dimension);

            for (int i = 0; i < sol.Length; i++)
            {
                Solution<T> s =  new Solution<T>(dimension);
                for (int j = 0; j < crossoverPoint; j++)
                {
                    s.DecisionVariables[j] = sol[i].DecisionVariables[j];
                }
                for (int j = crossoverPoint; j < dimension; j++)
                {
                    s.DecisionVariables[j] =
                        sol[(i + 1) % solutions.Count].DecisionVariables[j];
                }
                result.Add(s);
            }
            
            return result;
        }

    }
}
