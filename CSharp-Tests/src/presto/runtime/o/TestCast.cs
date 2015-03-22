using NUnit.Framework;
using presto.parser;
using presto.utils;

namespace presto.runtime.o
{

	[TestFixture]
	public class TestCast : BaseOParserTest
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
			CheckOutput("cast/autoDowncast.o");
		}

		[Test]
		public void testCastChild()
		{
			CheckOutput("cast/castChild.o");
		}

		[Test]
		public void testIsAChild()
		{
			CheckOutput("cast/isAChild.o");
		}

		[Test]
		public void testIsAText()
		{
			CheckOutput("cast/isAText.o");
		}

	}
}

