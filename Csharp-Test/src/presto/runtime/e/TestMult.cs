using NUnit.Framework;
using presto.parser;
using presto.utils;

namespace presto.runtime.e
{

	[TestFixture]
	public class TestMult : BaseEParserTest
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
			CheckOutput("mult/multCharacter.e");
		}

		[Test]
		public void testMultDecimal()
		{
			CheckOutput("mult/multDecimal.e");
		}

		[Test]
		public void testMultInteger()
		{
			CheckOutput("mult/multInteger.e");
		}

		[Test]
		public void testMultList()
		{
			CheckOutput("mult/multList.e");
		}

		[Test]
		public void testMultPeriod()
		{
			CheckOutput("mult/multPeriod.e");
		}

		[Test]
		public void testMultText()
		{
			CheckOutput("mult/multText.e");
		}

	}
}

