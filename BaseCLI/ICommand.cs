using System.Threading.Tasks;

namespace BaseCLI
{
    public interface ICommand<TArgs> where TArgs : new()
	{
        string Name { get; }
        string Description { get; }
        ArgInfoCollection<TArgs> ArgInfoCollection { get; }

		Task Execute(TArgs args);
	}
}