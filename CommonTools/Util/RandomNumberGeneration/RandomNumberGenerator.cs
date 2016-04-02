namespace CommonTools.Util.RandomNumberGeneration
{
    public abstract class RandomNumberGenerator
    {
        public abstract double NextDouble();

        public abstract double NextDouble(double maxValue);

        public abstract double NextDouble(double minValue, double maxValue);

        public abstract int Next();

        public abstract int Next(int maxValue);

        public abstract int Next(int minValue, int maxValue);
    }
}
