using NUnit.Framework;
using presto.parser;
using presto.utils;

namespace presto.runtime.o
{

	[TestFixture]
	public class TestSub : BaseOParserTest
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
		public void testSubDate()
		{
			CheckOutput("sub/subDate.poc");
		}

		[Test]
		public void testSubDateTime()
		{
			CheckOutput("sub/subDateTime.poc");
		}

		[Test]
		public void testSubDecimal()
		{
			CheckOutput("sub/subDecimal.poc");
		}

		[Test]
		public void testSubInteger()
		{
			CheckOutput("sub/subInteger.poc");
		}

		[Test]
		public void testSubPeriod()
		{
			CheckOutput("sub/subPeriod.poc");
		}

		[Test]
		public void testSubTime()
		{
			CheckOutput("sub/subTime.poc");
		}

	}
}

