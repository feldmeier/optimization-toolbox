namespace CommonTools.Common.InitializationSchemes
{
    public abstract class InitializationScheme<T>
    {
        public abstract void Initialize(Solution<T> solution);
    }
}
