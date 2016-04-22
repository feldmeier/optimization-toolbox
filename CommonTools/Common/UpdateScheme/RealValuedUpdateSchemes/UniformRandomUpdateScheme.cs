using CommonTools.Util.RandomNumberGeneration;

namespace CommonTools.Common.UpdateScheme
{
    public class UniformRandomUpdateScheme : UpdateScheme<double>
    {
        private readonly RandomNumberGenerator Random;

        private readonly double Range;

        public UniformRandomUpdateScheme(RandomNumberGenerator random, double range)
        {
            this.Random = random;
            this.Range = range;
        }

        public override Solution<double> Update(Solution<double>  old)
        {

            int index = this.Random.Next(old.Dimension);

            Solution<double> res = old.Copy();

            for (int i = 0; i < res.Dimension; i++)
            {
                if (i == index)
                {
                    res.DecisionVariables[i] = old.DecisionVariables[i] + this.Random.NextDouble(-this.Range, this.Range);
                }
                else
                {
                    res.DecisionVariables[i] = old.DecisionVariables[i];
                }
            }
            return res;
        }
    }
}
