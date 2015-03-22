using NUnit.Framework;
using presto.parser;
using presto.utils;

namespace presto.runtime.e
{

	[TestFixture]
	public class TestLogic : BaseEParserTest
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
			CheckOutput("logic/andBoolean.e");
		}

		[Test]
		public void testNotBoolean()
		{
			CheckOutput("logic/notBoolean.e");
		}

		[Test]
		public void testOrBoolean()
		{
			CheckOutput("logic/orBoolean.e");
		}

	}
}

