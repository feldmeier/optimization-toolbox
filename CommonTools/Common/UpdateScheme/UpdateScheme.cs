namespace CommonTools.Common.UpdateScheme
{
    public abstract class UpdateScheme<T>
    {
        public abstract Solution<T> Update(Solution<T> old);
    }
}
