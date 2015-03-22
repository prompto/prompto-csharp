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
			CheckOutput("sub/subDate.o");
		}

		[Test]
		public void testSubDateTime()
		{
			CheckOutput("sub/subDateTime.o");
		}

		[Test]
		public void testSubDecimal()
		{
			CheckOutput("sub/subDecimal.o");
		}

		[Test]
		public void testSubInteger()
		{
			CheckOutput("sub/subInteger.o");
		}

		[Test]
		public void testSubPeriod()
		{
			CheckOutput("sub/subPeriod.o");
		}

		[Test]
		public void testSubTime()
		{
			CheckOutput("sub/subTime.o");
		}

	}
}

