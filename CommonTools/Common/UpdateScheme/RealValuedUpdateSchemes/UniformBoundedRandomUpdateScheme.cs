using System;
using CommonTools.Util.RandomNumberGeneration;

namespace CommonTools.Common.UpdateScheme
{
    public class UniformBoundedRandomUpdateScheme : UpdateScheme<double>
    {
        private readonly RandomNumberGenerator Random;

        private readonly double LowerBound;
        private readonly double UpperBound;

        private readonly double Range;

        public UniformBoundedRandomUpdateScheme(RandomNumberGenerator random, double lowerBound, double upperBound, double range)
        {
            this.Random = random;
            this.LowerBound = lowerBound;
            this.UpperBound = upperBound;
            this.Range = range;
        }

        public override Solution<double> Update(Solution<double>  old)
        {

            int index = this.Random.Next(old.DecisionVariables.Length);

            Solution<double> res = old.Copy();

            for (int i = 0; i < res.DecisionVariables.Length; i++)
            {
                if (i == index)
                {
                    res.DecisionVariables[i] = old.DecisionVariables[i] + this.Random.NextDouble(-this.Range, this.Range);
                    res.DecisionVariables[i] = Math.Max(res.DecisionVariables[i], this.LowerBound);
                    res.DecisionVariables[i] = Math.Min(res.DecisionVariables[i], this.UpperBound);
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
