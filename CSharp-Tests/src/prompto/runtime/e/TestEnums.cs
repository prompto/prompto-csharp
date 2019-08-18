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
		public void testStoreCategoryEnum()
		{
			CheckOutput("enums/storeCategoryEnum.pec");
		}

		[Test]
		public void testStoreIntegerEnum()
		{
			CheckOutput("enums/storeIntegerEnum.pec");
		}

		[Test]
		public void testStoreTextEnum()
		{
			CheckOutput("enums/storeTextEnum.pec");
		}

		[Test]
		public void testTextEnum()
		{
			CheckOutput("enums/textEnum.pec");
		}

		[Test]
		public void testTextEnumArg()
		{
			CheckOutput("enums/textEnumArg.pec");
		}

		[Test]
		public void testTextEnumVar()
		{
			CheckOutput("enums/textEnumVar.pec");
		}

	}
}

