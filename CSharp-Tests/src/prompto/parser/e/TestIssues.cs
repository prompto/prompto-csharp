using prompto.parser;
using NUnit.Framework;
using prompto.utils;
namespace prompto.parser.e {

    [TestFixture]
    public class TestIssues : BaseEParserTest
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
        public void testMinimal()
        {
            runResource("issues/minimal.pec", "main", null);
        }
    }

}
