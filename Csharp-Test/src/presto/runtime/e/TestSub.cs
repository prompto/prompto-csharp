using NUnit.Framework;
using presto.parser;
using presto.utils;

namespace presto.runtime.e
{

	[TestFixture]
	public class TestSub : BaseEParserTest
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
			CheckOutput("sub/subDate.e");
		}

		[Test]
		public void testSubDateTime()
		{
			CheckOutput("sub/subDateTime.e");
		}

		[Test]
		public void testSubDecimal()
		{
			CheckOutput("sub/subDecimal.e");
		}

		[Test]
		public void testSubInteger()
		{
			CheckOutput("sub/subInteger.e");
		}

		[Test]
		public void testSubPeriod()
		{
			CheckOutput("sub/subPeriod.e");
		}

		[Test]
		public void testSubTime()
		{
			CheckOutput("sub/subTime.e");
		}

	}
}

