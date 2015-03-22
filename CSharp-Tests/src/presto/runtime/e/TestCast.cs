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
			CheckOutput("cast/autoDowncast.e");
		}

		[Test]
		public void testCastChild()
		{
			CheckOutput("cast/castChild.e");
		}

		[Test]
		public void testIsAChild()
		{
			CheckOutput("cast/isAChild.e");
		}

		[Test]
		public void testIsAText()
		{
			CheckOutput("cast/isAText.e");
		}

	}
}

