using System;
using System.Collections.Generic;
using NUnit.Framework;
using SimpleCLI.ArgInfoBuilder;

namespace SimpleCLI.Tests.ArgInfoBuilder
{
    public class ArgInfoBuilderTests
    {
        [Test]
        public void SetPropertyName_PredicateIsNull_Throws()
        {
            Assert.Throws<ArgInfoBuilderError>(() => CreateSetPropertyName().For<object>(null));
        }

        [Test]
        public void SetPropertyName_PredicateDoesNotSelectProperty_Throws()
        {
            Assert.Throws<ArgInfoBuilderError>(() => CreateSetPropertyName().For(x => ""));
        }

        [Test]
        public void SetPropertyName_PredicateSelectsProperty_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => CreateSetPropertyName().For(x => x.String));
        }

        [Test]
        public void SetArgInfoTypeCtor_PropertyNameDoesNotMatchAnyProperties_Throws()
        {
            Assert.Throws<ArgInfoBuilderError>(() => new global::SimpleCLI.ArgInfo.SetArgInfoType<TestArgs>("", "", "NotAProperty"));
        }

        [Test]
        public void SetArgInfoTypeCtor_MatchedPropertyNameHasNoSetter_Throws()
        {
            Assert.Throws<ArgInfoBuilderError>(() => new global::SimpleCLI.ArgInfo.SetArgInfoType<TestArgs>("", "", "NoSetter"));
        }

        [Test]
        public void SetArgInfoTypeAsString_PropertyNameDoesNotMatchToAString_Throws()
        {
            var setArgInfoType = global::SimpleCLI.ArgInfo
                .Create<TestArgs>("s", "s")
                .For(x => x.Int);
            Assert.Throws<ArgInfoBuilderError>(() => setArgInfoType.String());
        }

        [Test]
        public void SetArgInfoTypeAsString_PropertyNameMatchesToAString_DoesNotThrow()
        {
            var setArgInfoType = global::SimpleCLI.ArgInfo
                .Create<TestArgs>("s", "s")
                .For(x => x.String);
            Assert.DoesNotThrow(() => setArgInfoType.String());
        }

        [Test]
        public void SetArgInfoTypeAsInt_PropertyNameDoesNotMatchToAInt_Throws()
        {
            var setArgInfoType = global::SimpleCLI.ArgInfo
                .Create<TestArgs>("s", "s")
                .For(x => x.String);
            Assert.Throws<ArgInfoBuilderError>(() => setArgInfoType.Int());
        }

        [Test]
        public void SetArgInfoTypeAsInt_PropertyNameMatchesToAInt_DoesNotThrow()
        {
            var setArgInfoType = global::SimpleCLI.ArgInfo
                .Create<TestArgs>("s", "s")
                .For(x => x.Int);
            Assert.DoesNotThrow(() => setArgInfoType.Int());
        }

        [Test]
        public void SetArgInfoTypeAsStringCollection_PropertyNameDoesNotMatchToAStringCollection_Throws()
        {
            var setArgInfoType = global::SimpleCLI.ArgInfo
                .Create<TestArgs>("s", "s")
                .For(x => x.Int);
            Assert.Throws<ArgInfoBuilderError>(() => setArgInfoType.StringCollection());
        }

        [Test]
        public void SetArgInfoTypeAsStringCollection_PropertyNameMatchesToAStringCollection_DoesNotThrow()
        {
            var setArgInfoType = global::SimpleCLI.ArgInfo
                .Create<TestArgs>("s", "s")
                .For(x => x.StringCollection);
            Assert.DoesNotThrow(() => setArgInfoType.StringCollection());
        }

        [Test]
        public void SetArgInfoTypeAsIntCollection_PropertyNameDoesNotMatchToAIntCollection_Throws()
        {
            var setArgInfoType = global::SimpleCLI.ArgInfo
                .Create<TestArgs>("s", "s")
                .For(x => x.Int);
            Assert.Throws<ArgInfoBuilderError>(() => setArgInfoType.IntCollection());
        }

        [Test]
        public void SetArgInfoTypeAsIntCollection_PropertyNameMatchesToAIntCollection_DoesNotThrow()
        {
            var setArgInfoType = global::SimpleCLI.ArgInfo
                .Create<TestArgs>("s", "s")
                .For(x => x.IntCollection);
            Assert.DoesNotThrow(() => setArgInfoType.IntCollection());
        }

        [Test]
        public void SetArgInfoTypeAsBool_PropertyNameDoesNotMatchToABool_Throws()
        {
            var setArgInfoType = global::SimpleCLI.ArgInfo
                .Create<TestArgs>("s", "s")
                .For(x => x.Int);
            Assert.Throws<ArgInfoBuilderError>(() => setArgInfoType.Bool());
        }

        [Test]
        public void SetArgInfoTypeAsBool_PropertyNameMatchesToABool_DoesNotThrow()
        {
            var setArgInfoType = global::SimpleCLI.ArgInfo
                .Create<TestArgs>("s", "s")
                .For(x => x.Bool);
            Assert.DoesNotThrow(() => setArgInfoType.Bool());
        }

        private global::SimpleCLI.ArgInfo.SetPropertyName<TestArgs> CreateSetPropertyName() 
            => new("s", "hey");

        private class TestArgs
        {
            public string String { get; set; }
            public int Int { get; set; }
            public List<string> StringCollection { get; set; }
            public List<int> IntCollection { get; set; }
            public bool Bool { get; set; }
            public bool NoSetter { get; }
        }
    }
}
