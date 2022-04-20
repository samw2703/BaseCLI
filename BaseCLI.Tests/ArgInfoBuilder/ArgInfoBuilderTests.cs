using NUnit.Framework;

namespace SimpleCLI.Tests.ArgInfoBuilder
{
    public class ArgInfoBuilderTests
    {
        [Test]
        public void Add_FlagIsNull_Throws()
        {
            Assert.Throws<ArgInfoBuilderError>(() => new ArgInfoBuilder<TestArgs>().Add(null, ""));
        }

        [Test]
        public void Add_FlagIsEmptyString_Throws()
        {
            Assert.Throws<ArgInfoBuilderError>(() => new ArgInfoBuilder<TestArgs>().Add("", ""));
        }

        [Test]
        public void Add_FlagAlreadyExists_Throws()
        {
            const string flag = "s";

            var builder = new ArgInfoBuilder<TestArgs>()
                .Add(flag, "").ForString(x => x.Str);

            Assert.Throws<ArgInfoBuilderError>(() => builder.Add(flag, ""));
        }

        private class TestArgs
        {
            public string Str { get; set; }
        }
    }
}