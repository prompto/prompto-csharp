using NUnit.Framework;
using prompto.parser;
using prompto.utils;

namespace prompto.runtime.e
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
		public void testAnyId()
		{
			CheckOutput("native/anyId.pec");
		}

		[Test]
		public void testAnyText()
		{
			CheckOutput("native/anyText.pec");
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
		public void testCategoryReturn()
		{
			CheckOutput("native/categoryReturn.pec");
		}

		[Test]
		public void testMethod()
		{
			CheckOutput("native/method.pec");
		}

		[Test]
		public void testNow()
		{
			CheckOutput("native/now.pec");
		}

		[Test]
		public void testPrinter()
		{
			CheckOutput("native/printer.pec");
		}

	}
}

