namespace BaseCLI.Conductor
{
	internal interface IConsole
	{
		void WriteLine(string message);
	}

	internal class Console : IConsole
	{
		public void WriteLine(string message)
			=> System.Console.WriteLine(message);
	}
}