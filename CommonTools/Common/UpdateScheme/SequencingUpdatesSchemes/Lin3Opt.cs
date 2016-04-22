using CommonTools.Util.RandomNumberGeneration;

namespace CommonTools.Common.UpdateScheme.SequencingUpdatesSchemes
{
    public class Lin3Opt<T> : UpdateScheme<T>
    {
        private readonly RandomNumberGenerator Random;

        public Lin3Opt(RandomNumberGenerator random)
        {
            this.Random = random;
        }

        public override Solution<T> Update(Solution<T> old)
        {
            Solution<T> updatedSolution = old.Copy();
            int startIndex = this.Random.Next(updatedSolution.Dimension - 2);
            int endIndex = this.Random.Next(startIndex + 1, updatedSolution.Dimension);

            int between = this.Random.Next(startIndex + 1, endIndex);

            int length1 = between - startIndex;
            T[] snippet1 = new T[length1];
            for (int i = 0; i < length1; i++)
            {
                snippet1[i] = old.DecisionVariables[startIndex + i];
            }

            int length2 = endIndex - between;
            T[] snippet2 = new T[length2];
            for (int i = 0; i < length2; i++)
            {
                snippet2[i] = old.DecisionVariables[between + i];
            }

            //copy back
            for (int i = 0; i < length2; i++)
            {
                updatedSolution.DecisionVariables[startIndex + i] = snippet2[i];
            }

            for (int i = 0; i < length1; i++)
            {
                updatedSolution.DecisionVariables[startIndex + length2 + i] = snippet1[i];
            }

            return updatedSolution;
        }
    }
}
