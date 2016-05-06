using prompto.parser;
using NUnit.Framework;
using prompto.utils;
using prompto.error;
using prompto.runtime.utils;

namespace prompto.e.runtime
{

    [TestFixture]
    public class TestResource : BaseEParserTest
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
