using presto.parser;
using NUnit.Framework;
using presto.utils;
namespace presto.e.runtime {

    [TestFixture]
    public class TestNativePrint : BaseEParserTest
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
        public void testNativePrint()
        {
			runResource("native/print.pec");
			Assert.AreEqual(Out.read(), "name=IBM");
        }
    }

}
