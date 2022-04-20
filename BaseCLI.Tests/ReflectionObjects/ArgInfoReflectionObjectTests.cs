using System.Collections.Generic;
using System.Security.Principal;
using Moq;
using NUnit.Framework;
using SimpleCLI.ReflectionObjects;

namespace SimpleCLI.Tests.ReflectionObjects
{
    public class ArgInfoReflectionObjectTests
    {
        [Test]
        public void Ctor_ObjectIsNull_Throws()
        {
            Assert.Throws<InvalidReflectionObject>(() => new ArgInfoReflectionObject(null));
        }

        [Test]
        public void Ctor_ObjectIsNotArgInfo_Throws()
        {
            Assert.Throws<InvalidReflectionObject>(() => new ArgInfoReflectionObject(""));
        }

        [Test]
        public void Validate_CallsValidateOnUnderlyingArgInfo()
        {
            var argInfo = CreateArgInfo();
            new ArgInfoReflectionObject(argInfo).Validate(new List<string>());

            Assert.AreEqual(1, argInfo.ValidateCallCount);
        }

        [Test]
        public void Parse_CallsParseOnUnderlyingArgInfo()
        {
            var argInfo = CreateArgInfo();
            new ArgInfoReflectionObject(argInfo).Parse(new TestArgs(), new List<string>());

            Assert.AreEqual(1, argInfo.ParseCallCount);
        }

        [Test]
        public void GetHelp_CallsGetHelpOnUnderlyingArgInfo()
        {
            var argInfo = CreateArgInfo();
            var helpText = new ArgInfoReflectionObject(argInfo).GetHelp();

            Assert.True(helpText.Contains("acbeafe5-f5dc-433a-b4db-7f2493541148"));
        }

        private TestArgInfo CreateArgInfo() => new("s", "String", "Str");

        private class TestArgInfo : ArgInfo<TestArgs>
        {
            public int ValidateCallCount { get; private set; } = 0;
            public int ParseCallCount { get; private set; } = 0;

            public TestArgInfo(string flag, string friendlyName, string propertyName, bool mandatory = false) 
                : base(flag, friendlyName, propertyName, mandatory)
            {
            }

            protected override string TypeDescription { get; } = "acbeafe5-f5dc-433a-b4db-7f2493541148";
            internal override void Validate(List<string> args)
            {
                ValidateCallCount++;
            }

            internal override void Parse(TestArgs parsedArgs, List<string> args)
            {
                ParseCallCount++;
            }
        }

        private class TestArgs
        {
            public string Str { get; set; }
        }
    }
}
