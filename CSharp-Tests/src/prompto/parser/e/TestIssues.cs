using prompto.parser;
using NUnit.Framework;
using prompto.utils;
namespace prompto.e.runtime {

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
            runResource("issues/minimal.pec", "mainNoCmdLine", null);
        }
    }

}
