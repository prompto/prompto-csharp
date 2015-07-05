// generated: 2015-07-05T23:01:01.345
using NUnit.Framework;
using prompto.parser;
using prompto.utils;

namespace prompto.runtime.o
{

	[TestFixture]
	public class TestMult : BaseOParserTest
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
		public void testMultCharacter()
		{
			CheckOutput("mult/multCharacter.poc");
		}

		[Test]
		public void testMultDecimal()
		{
			CheckOutput("mult/multDecimal.poc");
		}

		[Test]
		public void testMultInteger()
		{
			CheckOutput("mult/multInteger.poc");
		}

		[Test]
		public void testMultList()
		{
			CheckOutput("mult/multList.poc");
		}

		[Test]
		public void testMultPeriod()
		{
			CheckOutput("mult/multPeriod.poc");
		}

		[Test]
		public void testMultText()
		{
			CheckOutput("mult/multText.poc");
		}

	}
}

