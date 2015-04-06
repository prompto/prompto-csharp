using presto.parser;
using NUnit.Framework;
using presto.error;
using presto.runtime.utils;
using presto.utils;

namespace presto.o.runtime {

    [TestFixture]
    public class TestResource : BaseOParserTest
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
            runResource("resource/badRead.poc");
        }

        [Test]
        [ExpectedException(typeof(SyntaxError))]
        public void testBadWrite()
        {
            runResource("resource/badWrite.poc");
        }

        [Test]
        [ExpectedException(typeof(SyntaxError))]
        public void testBadResource()
        {
            runResource("resource/badResource.poc");
        }

        [Test]
        public void testReadResource()
        {
            CheckOutput("resource/readResource.poc");
        }

        [Test]
        public void testWriteResource()
        {
            CheckOutput("resource/writeResource.poc");
        }

        [Test]
        public void testReadWithResource()
        {
            CheckOutput("resource/readWithResource.poc");
        }

        [Test]
        public void testWriteWithResource()
        {
            CheckOutput("resource/writeWithResource.poc");
        }

    }

}
