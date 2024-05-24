namespace Minerals.AutoCQRS
{
    public partial interface ICommand
    {

    }

    public partial interface ICommand<T> : ICommand where T : notnull
    {
        public T Id { get; }
    }
}