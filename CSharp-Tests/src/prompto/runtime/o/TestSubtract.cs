using NUnit.Framework;
using prompto.parser;
using prompto.utils;

namespace prompto.runtime.o
{

	[TestFixture]
	public class TestSubtract : BaseOParserTest
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
			CheckOutput("subtract/subDate.poc");
		}

		[Test]
		public void testSubDateTime()
		{
			CheckOutput("subtract/subDateTime.poc");
		}

		[Test]
		public void testSubDecimal()
		{
			CheckOutput("subtract/subDecimal.poc");
		}

		[Test]
		public void testSubInteger()
		{
			CheckOutput("subtract/subInteger.poc");
		}

		[Test]
		public void testSubList()
		{
			CheckOutput("subtract/subList.poc");
		}

		[Test]
		public void testSubPeriod()
		{
			CheckOutput("subtract/subPeriod.poc");
		}

		[Test]
		public void testSubSet()
		{
			CheckOutput("subtract/subSet.poc");
		}

		[Test]
		public void testSubTime()
		{
			CheckOutput("subtract/subTime.poc");
		}

	}
}

