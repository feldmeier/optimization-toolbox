using CommonTools.Util.RandomNumberGeneration;

namespace CommonTools.Common.InitializationSchemes.RealValued
{
    public class UniformBoundedInitializationScheme : RealValuedInitializationScheme
    {
        private readonly double LowerBound;
        private readonly double UpperBound;

        private readonly RandomNumberGenerator Random;

        public UniformBoundedInitializationScheme(RandomNumberGenerator random, double lowerBound, double upperBound)
        {
            this.Random = random;
            this.LowerBound = lowerBound;
            this.UpperBound = upperBound;
        }

        public override void Initialize(Solution<double> solution)
        {
            for (int i = 0; i < solution.DecisionVariables.Length; i++)
            {
                solution.DecisionVariables[i] = this.Random.NextDouble(this.LowerBound, this.UpperBound);
            }
        }
    }
}
