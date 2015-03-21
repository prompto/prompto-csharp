using NUnit.Framework;
using presto.parser;
using presto.utils;

namespace presto.runtime.o
{

	[TestFixture]
	public class TestLogic : BaseOParserTest
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
		public void testAndBoolean()
		{
			CheckOutput("logic/andBoolean.o");
		}

		[Test]
		public void testNotBoolean()
		{
			CheckOutput("logic/notBoolean.o");
		}

		[Test]
		public void testOrBoolean()
		{
			CheckOutput("logic/orBoolean.o");
		}

	}
}

