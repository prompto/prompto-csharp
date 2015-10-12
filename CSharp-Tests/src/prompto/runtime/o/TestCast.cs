using NUnit.Framework;
using prompto.parser;
using prompto.utils;

namespace prompto.runtime.o
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
			CheckOutput("cast/autoDowncast.poc");
		}

		[Test]
		public void testCastChild()
		{
			CheckOutput("cast/castChild.poc");
		}

		[Test]
		public void testIsAChild()
		{
			CheckOutput("cast/isAChild.poc");
		}

		[Test]
		public void testIsAText()
		{
			CheckOutput("cast/isAText.poc");
		}

	}
}

