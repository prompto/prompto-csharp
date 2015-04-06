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
			CheckOutput("mult/multCharacter.pec");
		}

		[Test]
		public void testMultDecimal()
		{
			CheckOutput("mult/multDecimal.pec");
		}

		[Test]
		public void testMultInteger()
		{
			CheckOutput("mult/multInteger.pec");
		}

		[Test]
		public void testMultList()
		{
			CheckOutput("mult/multList.pec");
		}

		[Test]
		public void testMultPeriod()
		{
			CheckOutput("mult/multPeriod.pec");
		}

		[Test]
		public void testMultText()
		{
			CheckOutput("mult/multText.pec");
		}

	}
}

