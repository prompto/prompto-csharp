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
			CheckOutput("sub/subDate.pec");
		}

		[Test]
		public void testSubDateTime()
		{
			CheckOutput("sub/subDateTime.pec");
		}

		[Test]
		public void testSubDecimal()
		{
			CheckOutput("sub/subDecimal.pec");
		}

		[Test]
		public void testSubInteger()
		{
			CheckOutput("sub/subInteger.pec");
		}

		[Test]
		public void testSubPeriod()
		{
			CheckOutput("sub/subPeriod.pec");
		}

		[Test]
		public void testSubTime()
		{
			CheckOutput("sub/subTime.pec");
		}

	}
}

