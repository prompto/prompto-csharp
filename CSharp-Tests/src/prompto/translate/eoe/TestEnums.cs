using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.eoe
{

	[TestFixture]
	public class TestEnums : BaseEParserTest
	{
        [Test]
		public void testCategoryEnum()
		{
			compareResourceEOE("enums/categoryEnum.pec");
		}
        
		[Test]
		public void testIntegerEnum()
		{
			compareResourceEOE("enums/integerEnum.pec");
		}

		[Test]
		public void testStoreCategoryEnum()
		{
			compareResourceEOE("enums/storeCategoryEnum.pec");
		}

		[Test]
		public void testStoreIntegerEnum()
		{
			compareResourceEOE("enums/storeIntegerEnum.pec");
		}

		[Test]
		public void testStoreTextEnum()
		{
			compareResourceEOE("enums/storeTextEnum.pec");
		}

		[Test]
		public void testTextEnum()
		{
			compareResourceEOE("enums/textEnum.pec");
		}

		[Test]
		public void testTextEnumArg()
		{
			compareResourceEOE("enums/textEnumArg.pec");
		}

		[Test]
		public void testTextEnumVar()
		{
			compareResourceEOE("enums/textEnumVar.pec");
		}

	}
}

