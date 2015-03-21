using NUnit.Framework;
using presto.parser;
using presto.utils;

namespace presto.runtime.o
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
			CheckOutput("mult/multCharacter.o");
		}

		[Test]
		public void testMultDecimal()
		{
			CheckOutput("mult/multDecimal.o");
		}

		[Test]
		public void testMultInteger()
		{
			CheckOutput("mult/multInteger.o");
		}

		[Test]
		public void testMultList()
		{
			CheckOutput("mult/multList.o");
		}

		[Test]
		public void testMultPeriod()
		{
			CheckOutput("mult/multPeriod.o");
		}

		[Test]
		public void testMultText()
		{
			CheckOutput("mult/multText.o");
		}

	}
}

