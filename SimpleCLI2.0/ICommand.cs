namespace SimpleCLI
{
    public interface ICommand<TArgs> where TArgs : new()
	{
        string Name { get; }
        string Description { get; }
        ArgInfoCollection<TArgs> ArgInfoCollection { get; }

		void Execute(TArgs args);
	}
}