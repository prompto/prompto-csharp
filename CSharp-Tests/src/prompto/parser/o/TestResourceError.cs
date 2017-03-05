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
        public void testBadRead()
        {
			Assert.Throws<SyntaxError>(() =>
			{
				runResource("resourceError/badRead.poc");
			});
        }

        [Test]
        public void testBadWrite()
        {
			Assert.Throws<SyntaxError>(() =>
			{
				runResource("resourceError/badWrite.poc");
			});
        }

        [Test]
        public void testBadResource()
        {
			Assert.Throws<SyntaxError>(() =>
			{
				runResource("resourceError/badResource.poc");
			});
        }

    }

}
