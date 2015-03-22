using NUnit.Framework;
using presto.parser;
using presto.utils;

namespace presto.runtime.e
{

	[TestFixture]
	public class TestEnums : BaseEParserTest
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
			CheckOutput("enums/categoryEnum.e");
		}

		[Test]
		public void testIntegerEnum()
		{
			CheckOutput("enums/integerEnum.e");
		}

		[Test]
		public void testTextEnum()
		{
			CheckOutput("enums/textEnum.e");
		}

	}
}

