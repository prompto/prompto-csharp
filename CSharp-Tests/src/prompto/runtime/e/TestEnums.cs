// generated: 2015-07-05T23:01:01.237
using NUnit.Framework;
using prompto.parser;
using prompto.utils;

namespace prompto.runtime.e
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
			CheckOutput("enums/categoryEnum.pec");
		}

		[Test]
		public void testIntegerEnum()
		{
			CheckOutput("enums/integerEnum.pec");
		}

		[Test]
		public void testTextEnum()
		{
			CheckOutput("enums/textEnum.pec");
		}

	}
}

