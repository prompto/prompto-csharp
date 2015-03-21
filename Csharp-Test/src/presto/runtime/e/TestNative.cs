using NUnit.Framework;
using presto.parser;
using presto.utils;

namespace presto.runtime.e
{

	[TestFixture]
	public class TestNative : BaseEParserTest
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
		public void testCategory()
		{
			CheckOutput("native/category.e");
		}

		[Test]
		public void testMethod()
		{
			CheckOutput("native/method.e");
		}

	}
}

