using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.eme
{

	[TestFixture]
	public class TestEnums : BaseEParserTest
	{
        [Test]
		public void testCategoryEnum()
		{
			compareResourceEME("enums/categoryEnum.pec");
		}
        

		[Test]
		public void testIntegerEnum()
		{
			compareResourceEME("enums/integerEnum.pec");
		}

		[Test]
		public void testStoreCategoryEnum()
		{
			compareResourceEME("enums/storeCategoryEnum.pec");
		}

		[Test]
		public void testStoreIntegerEnum()
		{
			compareResourceEME("enums/storeIntegerEnum.pec");
		}

		[Test]
		public void testStoreTextEnum()
		{
			compareResourceEME("enums/storeTextEnum.pec");
		}

		[Test]
		public void testTextEnum()
		{
			compareResourceEME("enums/textEnum.pec");
		}

		[Test]
		public void testTextEnumArg()
		{
			compareResourceEME("enums/textEnumArg.pec");
		}

		[Test]
		public void testTextEnumVar()
		{
			compareResourceEME("enums/textEnumVar.pec");
		}

	}
}

