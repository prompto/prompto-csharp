using NUnit.Framework;
using prompto.parser;
using prompto.utils;

namespace prompto.runtime.e
{

	[TestFixture]
	public class TestIterate : BaseEParserTest
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
		public void testForEachCategoryList()
		{
			CheckOutput("iterate/forEachCategoryList.pec");
		}

		[Test]
		public void testForEachExpression()
		{
			CheckOutput("iterate/forEachExpression.pec");
		}

		[Test]
		public void testForEachIntegerList()
		{
			CheckOutput("iterate/forEachIntegerList.pec");
		}

	}
}

