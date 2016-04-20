using CommonTools.Util.RandomNumberGeneration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTools.Common.InitializationSchemes.Sequencing
{
    public class RandomSequencingInitializationScheme : InitializationScheme<int>
    {
        private readonly RandomNumberGenerator Random;

        public RandomSequencingInitializationScheme(RandomNumberGenerator random)
        {
            this.Random = random;
        }
        public override void Initialize(Solution<int> solution)
        {
            List<int> sequence = new List<int>();
            for (int i = 0; i < solution.Dimension; i++)
            {
                int position = this.Random.Next(i + 1);
                sequence.Insert(position, i);
            }
            solution.DecisionVariables = sequence.ToArray();
        }
    }
}
