using CommonTools.Util.RandomNumberGeneration;

namespace CommonTools.Common.UpdateScheme.SequencingUpdatesSchemes
{
    public class Lin2Opt<T> : UpdateScheme<T>
    {
        private readonly RandomNumberGenerator Random;

        public Lin2Opt(RandomNumberGenerator random)
        {
            this.Random = random;
        }

        public override Solution<T> Update(Solution<T> old)
        {
            Solution<T> updatedSolution = old.Copy();
            int index1 = this.Random.Next(updatedSolution.Dimension - 1);
            int index2 = this.Random.Next(index1 + 1, updatedSolution.Dimension);


            int length = index2 - index1;

            //get snippet
            T[] snippet = new T[length];
            for (int i = 0; i < length; i++)
            {
                snippet[i] = old.DecisionVariables[index1 + i];
            }

            //turn snippet around
            T[] turned = new T[length];
            for (int i = 0; i < length; i++)
            {
                turned[i] = snippet[length - i - 1];
            }

            //copy back
            for (int i = 0; i < length; i++)
            {
                updatedSolution.DecisionVariables[index1 + i] = turned[i];
            }

            return updatedSolution;
        }
    }
}
