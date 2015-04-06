using NUnit.Framework;
using presto.parser;
using presto.utils;

namespace presto.runtime.e
{

	[TestFixture]
	public class TestCast : BaseEParserTest
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
		public void testAutoDowncast()
		{
			CheckOutput("cast/autoDowncast.pec");
		}

		[Test]
		public void testCastChild()
		{
			CheckOutput("cast/castChild.pec");
		}

		[Test]
		public void testIsAChild()
		{
			CheckOutput("cast/isAChild.pec");
		}

		[Test]
		public void testIsAText()
		{
			CheckOutput("cast/isAText.pec");
		}

	}
}

