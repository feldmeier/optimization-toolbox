using System;

namespace CommonTools.Util.RandomNumberGeneration
{
    public class StandardRandomNumberGenerator : RandomNumberGenerator
    {
        private readonly Random Random;

        public StandardRandomNumberGenerator()
        {
            this.Random = new Random();
        }

        public override double NextDouble()
        {
            return this.Random.NextDouble();
        }

        public override double NextDouble(double maxValue)
        {
            return this.Random.NextDouble()*maxValue;
        }

        public override double NextDouble(double minValue, double maxValue)
        {
            return this.Random.NextDouble()*(maxValue - minValue) + minValue;
        }

        public override int Next()
        {
            return this.Random.Next();
        }

        public override int Next(int maxValue)
        {
            return this.Random.Next(maxValue);
        }

        public override int Next(int minValue, int maxValue)
        {
            return this.Random.Next(minValue, maxValue);
        }
    }
}
