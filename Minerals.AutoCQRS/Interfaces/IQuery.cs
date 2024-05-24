namespace Minerals.AutoCQRS.Interfaces
{
    public partial interface IQuery
    {

    }

    public partial interface IQuery<T> : IQuery where T : notnull
    {
        public T Id { get; }
    }
}