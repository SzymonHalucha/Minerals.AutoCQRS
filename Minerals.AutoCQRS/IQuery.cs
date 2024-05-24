namespace Minerals.AutoCQRS
{
    public partial interface IQuery
    {

    }

    public partial interface IQuery<T> : IQuery where T : notnull
    {
        public T Id { get; }
    }
}