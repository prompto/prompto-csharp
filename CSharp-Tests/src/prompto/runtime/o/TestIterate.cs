using NUnit.Framework;
using prompto.parser;
using prompto.utils;

namespace prompto.runtime.o
{

	[TestFixture]
	public class TestIterate : BaseOParserTest
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
		public void testForEachExpression()
		{
			CheckOutput("iterate/forEachExpression.poc");
		}

		[Test]
		public void testForEachIntegerList()
		{
			CheckOutput("iterate/forEachIntegerList.poc");
		}

		[Test]
		public void testForEachIntegerRange()
		{
			CheckOutput("iterate/forEachIntegerRange.poc");
		}

		[Test]
		public void testForEachIntegerSet()
		{
			CheckOutput("iterate/forEachIntegerSet.poc");
		}

	}
}

