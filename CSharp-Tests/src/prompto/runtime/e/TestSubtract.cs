using NUnit.Framework;
using prompto.parser;
using prompto.utils;

namespace prompto.runtime.e
{

	[TestFixture]
	public class TestSubtract : BaseEParserTest
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
			CheckOutput("subtract/subDate.pec");
		}

		[Test]
		public void testSubDateTime()
		{
			CheckOutput("subtract/subDateTime.pec");
		}

		[Test]
		public void testSubDecimal()
		{
			CheckOutput("subtract/subDecimal.pec");
		}

		[Test]
		public void testSubDecimalEnum()
		{
			CheckOutput("subtract/subDecimalEnum.pec");
		}

		[Test]
		public void testSubInteger()
		{
			CheckOutput("subtract/subInteger.pec");
		}

		[Test]
		public void testSubIntegerEnum()
		{
			CheckOutput("subtract/subIntegerEnum.pec");
		}

		[Test]
		public void testSubList()
		{
			CheckOutput("subtract/subList.pec");
		}

		[Test]
		public void testSubPeriod()
		{
			CheckOutput("subtract/subPeriod.pec");
		}

		[Test]
		public void testSubSet()
		{
			CheckOutput("subtract/subSet.pec");
		}

		[Test]
		public void testSubTime()
		{
			CheckOutput("subtract/subTime.pec");
		}

	}
}

