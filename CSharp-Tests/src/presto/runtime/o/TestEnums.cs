using NUnit.Framework;
using presto.parser;
using presto.utils;

namespace presto.runtime.o
{

	[TestFixture]
	public class TestEnums : BaseOParserTest
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
		public void testCategoryEnum()
		{
			CheckOutput("enums/categoryEnum.poc");
		}

		[Test]
		public void testIntegerEnum()
		{
			CheckOutput("enums/integerEnum.poc");
		}

		[Test]
		public void testTextEnum()
		{
			CheckOutput("enums/textEnum.poc");
		}

	}
}

