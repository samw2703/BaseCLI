using System;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace BaseCLI
{
    class Program
    {
        static void Main(string[] args)
        {
	        SimpleCLI.SimpleCLI.Execute(args, GetAssemblies(), Setup);
        }

        private static Assembly[] GetAssemblies()
	        => new[] {Assembly.GetAssembly(typeof(TestCommand))};

        private static void Setup(ServiceCollection sc)
        {
	        sc.AddSingleton<IHelloSayer, HelloSayer>();
        }

    }

    public interface IHelloSayer
    {
	    void SayHello();
    }

    public class HelloSayer : IHelloSayer
    {
	    public void SayHello()
	    {
		    Console.WriteLine("Hello!");
	    }
    }
}
