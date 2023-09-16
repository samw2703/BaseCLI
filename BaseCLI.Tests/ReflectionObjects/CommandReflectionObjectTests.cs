using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using BaseCLI.ReflectionObjects;
using NUnit.Framework;

namespace BaseCLI.Tests.ReflectionObjects
{
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
            Assert.AreEqual(Count(testCommand.ArgInfoCollection), reflectionObject.ArgInfos.Count);
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

        private int Count(IEnumerable enumerable)
        {
            var count = 0;
            foreach (var item in enumerable)
                count++;

            return count;
        }

        private class TestCommand : ICommand<TestArgs>
        {
            public string Name { get; } = "Test Name";
            public string Description { get; } = "Test Description";
            public ArgInfoCollection<TestArgs> ArgInfoCollection { get; } = new ArgInfoCollection<TestArgs>(
                new List<ArgInfo<TestArgs>>
                {
                    new StringArgInfo<TestArgs>("s", "S", "Str"),
                    new IntArgInfo<TestArgs>("i", "I", "Int")
                });

            public int ExecuteCallCount { get; private set; } = 0;

            public Task Execute(TestArgs args)
            {
                ExecuteCallCount++;
                return Task.CompletedTask;
            }
        }

        private class TestArgs
        {
            public string Str { get; set; }
            public int Int { get; set; }
        }
    }
}