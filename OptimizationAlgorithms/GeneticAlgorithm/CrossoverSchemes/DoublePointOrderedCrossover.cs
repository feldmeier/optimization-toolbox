using CommonTools.Common;
using CommonTools.Util.RandomNumberGeneration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OptimizationAlgorithms.GeneticAlgorithm.CrossoverSchemes
{
    public class DoublePointOrderedCrossover : CrossoverScheme<int>
    {
        private readonly RandomNumberGenerator Random;

        public DoublePointOrderedCrossover(RandomNumberGenerator random)
        {
            this.Random = random;        
        }

        public override Solution<int> Crossover(Solution<int> sol1, Solution<int> sol2)
        {
            Solution<int> res = sol1.Copy();
            int index1 = this.Random.Next(res.Dimension);
            int index2 = this.Random.Next(index1, res.Dimension);

            res = this.Crossover(sol1, sol2, index1, index2);
            return res;
        }

        public Solution<int> Crossover(Solution<int> sol1, Solution<int> sol2, int index1, int index2)
        {
            Solution<int> res = sol1.Copy();

            List<int> sequence = new List<int>();
            for (int i = 0; i < sol2.Dimension; i++)
            {
                for (int j = index1; j < index2; j++)
                {
                    if (sol1.DecisionVariables[j] == sol2.DecisionVariables[i])
                    {
                        sequence.Add(sol2.DecisionVariables[i]);
                    }
                }
            }
            //merge sequences
            for (int i = index1; i < index2; i++)
            {
                res.DecisionVariables[i] = sequence[0];
                sequence.RemoveAt(0);
            }
            return res;
        }
    }
}
