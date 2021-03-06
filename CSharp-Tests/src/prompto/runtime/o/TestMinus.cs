using NUnit.Framework;
using prompto.parser;
using prompto.utils;

namespace prompto.runtime.o
{

	[TestFixture]
	public class TestMinus : BaseOParserTest
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
			CheckOutput("minus/minusDecimal.poc");
		}

		[Test]
		public void testMinusInteger()
		{
			CheckOutput("minus/minusInteger.poc");
		}

		[Test]
		public void testMinusPeriod()
		{
			CheckOutput("minus/minusPeriod.poc");
		}

	}
}

