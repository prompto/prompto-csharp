using NUnit.Framework;
using prompto.parser;
using prompto.utils;

namespace prompto.runtime.o
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
		public void testSwitchEnum()
		{
			CheckOutput("enums/switchEnum.poc");
		}

		[Test]
		public void testTextEnum()
		{
			CheckOutput("enums/textEnum.poc");
		}

		[Test]
		public void testTextEnumArg()
		{
			CheckOutput("enums/textEnumArg.poc");
		}

		[Test]
		public void testTextEnumVar()
		{
			CheckOutput("enums/textEnumVar.poc");
		}

	}
}

