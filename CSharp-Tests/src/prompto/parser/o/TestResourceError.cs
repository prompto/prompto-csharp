using NUnit.Framework;
using prompto.error;
using prompto.utils;

namespace prompto.parser.o {

    [TestFixture]
    public class TestResourceError : BaseOParserTest
    {

        [SetUp]
        public void before()
        {
            Out.init();
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
