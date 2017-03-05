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
		public void testBadRead()
		{
			Assert.Throws<SyntaxError>(() =>
			{
				runResource("resourceError/badRead.pec");
			});
       }

        [Test]
        public void testBadWrite()
        {
			Assert.Throws<SyntaxError>(() =>
			{
				runResource("resourceError/badWrite.pec");
			});
        }

        [Test]
        public void testBadResource()
        {
			Assert.Throws<SyntaxError>(() =>
			{
				runResource("resourceError/badResource.pec");
			});
        }

    }

}
