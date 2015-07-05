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
            runResource("resource/badRead.pec");
        }

        [Test]
        [ExpectedException(typeof(SyntaxError))]
        public void testBadWrite()
        {
            runResource("resource/badWrite.pec");
        }

        [Test]
        [ExpectedException(typeof(SyntaxError))]
        public void testBadResource()
        {
            runResource("resource/badResource.pec");
        }

        [Test]
        public void testReadResource()
        {
            CheckOutput("resource/readResource.pec");
        }

        [Test]
        public void testWriteResource()
        {
            CheckOutput("resource/writeResource.pec");
        }

        [Test]
        public void testReadWithResource()
        {
            CheckOutput("resource/readWithResource.pec");
        }

        [Test]
        public void testWriteWithResource()
        {
            CheckOutput("resource/writeWithResource.pec");
        }

    }

}