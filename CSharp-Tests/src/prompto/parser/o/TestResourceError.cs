using prompto.parser;
using NUnit.Framework;
using prompto.error;
using prompto.runtime.utils;
using prompto.utils;

namespace prompto.o.runtime {

    [TestFixture]
    public class TestResourceError : BaseOParserTest
    {

        [SetUp]
        public void before()
        {
            Out.init();
            MyResource.content = "readFullyOk";
        }

        [TearDown]
        public void after()
        {
            Out.restore();
        }

        [Test]
        [ExpectedException(typeof(SyntaxError))]
        public void testBadRead()
        {
            runResource("resourceError/badRead.poc");
        }

        [Test]
        [ExpectedException(typeof(SyntaxError))]
        public void testBadWrite()
        {
			runResource("resourceError/badWrite.poc");
        }

        [Test]
        [ExpectedException(typeof(SyntaxError))]
        public void testBadResource()
        {
			runResource("resourceError/badResource.poc");
        }

    }

}
