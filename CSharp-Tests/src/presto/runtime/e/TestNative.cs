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
		public void testAttribute()
		{
			CheckOutput("native/attribute.pec");
		}

		[Test]
		public void testCategory()
		{
			CheckOutput("native/category.pec");
		}

		[Test]
		public void testMethod()
		{
			CheckOutput("native/method.pec");
		}

		[Test]
		public void testPrinter()
		{
			CheckOutput("native/printer.pec");
		}

	}
}

