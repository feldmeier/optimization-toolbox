using CommonTools.Util.RandomNumberGeneration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTools.Common.UpdateScheme
{
    public class SwapElementsUpdateScheme<T> : UpdateScheme<T>
    {
        private readonly RandomNumberGenerator Random;

        public SwapElementsUpdateScheme(RandomNumberGenerator random)
        {
            this.Random = random;
        }

        public override Solution<T> Update(Solution<T> old)
        {
            Solution<T> updatedSolution = old.Copy();
            int index1 = this.Random.Next(updatedSolution.Dimension);
            int index2 = this.Random.Next(updatedSolution.Dimension);
            updatedSolution.DecisionVariables[index1] = old.DecisionVariables[index2];
            updatedSolution.DecisionVariables[index2] = old.DecisionVariables[index1];
            return updatedSolution;
        }
    }
}
