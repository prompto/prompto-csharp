using presto.parser;
using NUnit.Framework;
using presto.utils;
namespace presto.e.runtime {

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
            runResource("issues/minimal.e", "mainNoCmdLine", null);
        }
    }

}
