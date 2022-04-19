using System.Collections.Generic;
using NUnit.Framework;
using SimpleCLI;
using SimpleCLI.ReflectionObjects;

public class CommandReflectionObjectTests
{
    [Test]
    public void Ctor_ObjectIsNull_Throws()
    {
        Assert.Throws<InvalidReflectionObject>(() => new CommandReflectionObject(null));
    }

    [Test]
    public void Ctor_ObjectIsNotCommand_Throws()
    {
        Assert.Throws<InvalidReflectionObject>(() => new CommandReflectionObject(""));
    }

    [Test]
    public void Ctor_SetsAllProperties()
    {
        var testCommand = new TestCommand();
        var reflectionObject = new CommandReflectionObject(testCommand);

        Assert.AreEqual(testCommand.Name, reflectionObject.Name);
        Assert.AreEqual(testCommand.Description, reflectionObject.Description);
        Assert.AreEqual(testCommand.ArgInfos.Count, reflectionObject.ArgInfos.Count);
        Assert.AreEqual(testCommand.GetType(), reflectionObject.CommandType);
    }

    [Test]
    public void Execute_CallsExecuteOnUnderlyingObject()
    {
        var testCommand = new TestCommand();
        var reflectionObject = new CommandReflectionObject(testCommand);

        reflectionObject.Execute(new TestArgs());

        Assert.AreEqual(1, testCommand.ExecuteCallCount);
    }

    private class TestCommand : ICommand<TestArgs>
    {
        public string Name { get; } = "Test Name";
        public string Description { get; } = "Test Description";

        public List<ArgInfo<TestArgs>> ArgInfos { get; } = new List<ArgInfo<TestArgs>>
        {
            new StringArgInfo<TestArgs>("s", "S", "Str"),
            new IntArgInfo<TestArgs>("i", "I", "Int")
        };

        public int ExecuteCallCount { get; private set; } = 0;

        public void Execute(TestArgs args)
        {
            ExecuteCallCount++;
        }
    }

    private class TestArgs
    {
        public string Str { get; set; }
        public int Int { get; set; }
    }
}