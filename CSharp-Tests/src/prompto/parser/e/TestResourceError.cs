using prompto.parser;
using NUnit.Framework;
using prompto.utils;
using prompto.error;

namespace prompto.parser.e
{

    [TestFixture]
    public class TestResourceError : BaseEParserTest
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
            runResource("resourceError/badRead.pec");
        }

        [Test]
        [ExpectedException(typeof(SyntaxError))]
        public void testBadWrite()
        {
			runResource("resourceError/badWrite.pec");
        }

        [Test]
        [ExpectedException(typeof(SyntaxError))]
        public void testBadResource()
        {
			runResource("resourceError/badResource.pec");
        }

    }

}
