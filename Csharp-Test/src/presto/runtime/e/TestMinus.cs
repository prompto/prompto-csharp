using NUnit.Framework;
using presto.parser;
using presto.utils;

namespace presto.runtime.e
{

	[TestFixture]
	public class TestMinus : BaseEParserTest
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
		public void testMinusDecimal()
		{
			CheckOutput("minus/minusDecimal.e");
		}

		[Test]
		public void testMinusInteger()
		{
			CheckOutput("minus/minusInteger.e");
		}

		[Test]
		public void testMinusPeriod()
		{
			CheckOutput("minus/minusPeriod.e");
		}

	}
}

