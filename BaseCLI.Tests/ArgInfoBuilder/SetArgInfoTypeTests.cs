using System.Collections.Generic;
using NUnit.Framework;

namespace BaseCLI.Tests.ArgInfoBuilder
{
    public class SetArgInfoTypeTests
    {
        [Test]
        public void ForString_PredicateIsNull_Throws()
        {
            var setArgInfoType = CreateSetArgInfoType();

            Assert.Throws<ArgInfoBuilderError>(() => setArgInfoType.ForString(null));
        }

        [Test]
        public void ForString_PredicateDoesNotSelectProperty_Throws()
        {
            var setArgInfoType = CreateSetArgInfoType();

            Assert.Throws<ArgInfoBuilderError>(() => setArgInfoType.ForString(x => ""));
        }

        [Test]
        public void ForString_MatchedPropertyHasNoSetter_Throws()
        {
            var setArgInfoType = CreateSetArgInfoType();

            Assert.Throws<ArgInfoBuilderError>(() => setArgInfoType.ForString(x => x.StringNoSetter));
        }

        [Test]
        public void ForMandatoryString_PredicateIsNull_Throws()
        {
            var setArgInfoType = CreateSetArgInfoType();

            Assert.Throws<ArgInfoBuilderError>(() => setArgInfoType.ForMandatoryString(null));
        }

        [Test]
        public void ForMandatoryString_PredicateDoesNotSelectProperty_Throws()
        {
            var setArgInfoType = CreateSetArgInfoType();

            Assert.Throws<ArgInfoBuilderError>(() => setArgInfoType.ForMandatoryString(x => ""));
        }

        [Test]
        public void ForMandatoryString_MatchedPropertyHasNoSetter_Throws()
        {
            var setArgInfoType = CreateSetArgInfoType();

            Assert.Throws<ArgInfoBuilderError>(() => setArgInfoType.ForMandatoryString(x => x.StringNoSetter));
        }

        [Test]
        public void ForInt_PredicateIsNull_Throws()
        {
            var setArgInfoType = CreateSetArgInfoType();

            Assert.Throws<ArgInfoBuilderError>(() => setArgInfoType.ForInt(null));
        }

        [Test]
        public void ForInt_PredicateDoesNotSelectProperty_Throws()
        {
            var setArgInfoType = CreateSetArgInfoType();

            Assert.Throws<ArgInfoBuilderError>(() => setArgInfoType.ForInt(x => 0));
        }

        [Test]
        public void ForInt_MatchedPropertyHasNoSetter_Throws()
        {
            var setArgInfoType = CreateSetArgInfoType();

            Assert.Throws<ArgInfoBuilderError>(() => setArgInfoType.ForInt(x => x.IntNoSetter));
        }

        [Test]
        public void ForMandatoryInt_PredicateIsNull_Throws()
        {
            var setArgInfoType = CreateSetArgInfoType();

            Assert.Throws<ArgInfoBuilderError>(() => setArgInfoType.ForMandatoryInt(null));
        }

        [Test]
        public void ForMandatoryInt_PredicateDoesNotSelectProperty_Throws()
        {
            var setArgInfoType = CreateSetArgInfoType();

            Assert.Throws<ArgInfoBuilderError>(() => setArgInfoType.ForMandatoryInt(x => 0));
        }

        [Test]
        public void ForMandatoryInt_MatchedPropertyHasNoSetter_Throws()
        {
            var setArgInfoType = CreateSetArgInfoType();

            Assert.Throws<ArgInfoBuilderError>(() => setArgInfoType.ForMandatoryInt(x => x.IntNoSetter));
        }

        [Test]
        public void ForStringCollection_PredicateIsNull_Throws()
        {
            var setArgInfoType = CreateSetArgInfoType();

            Assert.Throws<ArgInfoBuilderError>(() => setArgInfoType.ForStringCollection(null));
        }

        [Test]
        public void ForStringCollection_PredicateDoesNotSelectProperty_Throws()
        {
            var setArgInfoType = CreateSetArgInfoType();

            Assert.Throws<ArgInfoBuilderError>(() => setArgInfoType.ForStringCollection(x => new List<string>()));
        }

        [Test]
        public void ForStringCollection_MatchedPropertyHasNoSetter_Throws()
        {
            var setArgInfoType = CreateSetArgInfoType();

            Assert.Throws<ArgInfoBuilderError>(() => setArgInfoType.ForStringCollection(x => x.StringCollectionNoSetter));
        }

        [Test]
        public void ForMandatoryStringCollection_PredicateIsNull_Throws()
        {
            var setArgInfoType = CreateSetArgInfoType();

            Assert.Throws<ArgInfoBuilderError>(() => setArgInfoType.ForMandatoryStringCollection(null));
        }

        [Test]
        public void ForMandatoryStringCollection_PredicateDoesNotSelectProperty_Throws()
        {
            var setArgInfoType = CreateSetArgInfoType();

            Assert.Throws<ArgInfoBuilderError>(() => setArgInfoType.ForMandatoryStringCollection(x => new List<string>()));
        }

        [Test]
        public void ForMandatoryStringCollection_MatchedPropertyHasNoSetter_Throws()
        {
            var setArgInfoType = CreateSetArgInfoType();

            Assert.Throws<ArgInfoBuilderError>(() => setArgInfoType.ForMandatoryStringCollection(x => x.StringCollectionNoSetter));
        }

        [Test]
        public void ForIntCollection_PredicateIsNull_Throws()
        {
            var setArgInfoType = CreateSetArgInfoType();

            Assert.Throws<ArgInfoBuilderError>(() => setArgInfoType.ForIntCollection(null));
        }

        [Test]
        public void ForIntCollection_PredicateDoesNotSelectProperty_Throws()
        {
            var setArgInfoType = CreateSetArgInfoType();

            Assert.Throws<ArgInfoBuilderError>(() => setArgInfoType.ForIntCollection(x => new List<int>()));
        }

        [Test]
        public void ForIntCollection_MatchedPropertyHasNoSetter_Throws()
        {
            var setArgInfoType = CreateSetArgInfoType();

            Assert.Throws<ArgInfoBuilderError>(() => setArgInfoType.ForIntCollection(x => x.IntCollectionNoSetter));
        }

        [Test]
        public void ForMandatoryIntCollection_PredicateIsNull_Throws()
        {
            var setArgInfoType = CreateSetArgInfoType();

            Assert.Throws<ArgInfoBuilderError>(() => setArgInfoType.ForMandatoryIntCollection(null));
        }

        [Test]
        public void ForMandatoryIntCollection_PredicateDoesNotSelectProperty_Throws()
        {
            var setArgInfoType = CreateSetArgInfoType();

            Assert.Throws<ArgInfoBuilderError>(() => setArgInfoType.ForMandatoryIntCollection(x => new List<int>()));
        }

        [Test]
        public void ForMandatoryIntCollection_MatchedPropertyHasNoSetter_Throws()
        {
            var setArgInfoType = CreateSetArgInfoType();

            Assert.Throws<ArgInfoBuilderError>(() => setArgInfoType.ForMandatoryIntCollection(x => x.IntCollectionNoSetter));
        }

        [Test]
        public void ForBool_PredicateIsNull_Throws()
        {
            var setArgInfoType = CreateSetArgInfoType();

            Assert.Throws<ArgInfoBuilderError>(() => setArgInfoType.ForBool(null));
        }

        [Test]
        public void ForBool_PredicateDoesNotSelectProperty_Throws()
        {
            var setArgInfoType = CreateSetArgInfoType();

            Assert.Throws<ArgInfoBuilderError>(() => setArgInfoType.ForBool(x => true));
        }

        [Test]
        public void ForBool_MatchedPropertyHasNoSetter_Throws()
        {
            var setArgInfoType = CreateSetArgInfoType();

            Assert.Throws<ArgInfoBuilderError>(() => setArgInfoType.ForBool(x => x.BoolNoSetter));
        }

        private SetArgInfoType<TestArgs> CreateSetArgInfoType() => new ArgInfoBuilder<TestArgs>().Add("test", "");

        private class TestArgs
        {
            public string StringNoSetter { get; }
            public int IntNoSetter { get; }
            public List<string> StringCollectionNoSetter { get; }
            public List<int> IntCollectionNoSetter { get; }
            public bool BoolNoSetter { get; }
        }
    }
}