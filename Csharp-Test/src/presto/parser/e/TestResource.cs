using presto.parser;
using NUnit.Framework;
using presto.utils;
using presto.error;
using presto.runtime.utils;

namespace presto.e.runtime
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
            runResource("resource/badRead.e");
        }

        [Test]
        [ExpectedException(typeof(SyntaxError))]
        public void testBadWrite()
        {
            runResource("resource/badWrite.e");
        }

        [Test]
        [ExpectedException(typeof(SyntaxError))]
        public void testBadResource()
        {
            runResource("resource/badResource.e");
        }

        [Test]
        public void testReadResource()
        {
            CheckOutput("resource/readResource.e");
        }

        [Test]
        public void testWriteResource()
        {
            CheckOutput("resource/writeResource.e");
        }

        [Test]
        public void testReadWithResource()
        {
            CheckOutput("resource/readWithResource.e");
        }

        [Test]
        public void testWriteWithResource()
        {
            CheckOutput("resource/writeWithResource.e");
        }

    }

}
