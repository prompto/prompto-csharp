using NUnit.Framework;
using presto.parser;

namespace presto.translate.epe
{

	[TestFixture]
	public class TestEnums : BaseEParserTest
	{

		[Test]
		public void testCategoryEnum()
		{
			compareResourceEPE("enums/categoryEnum.e");
		}

		[Test]
		public void testIntegerEnum()
		{
			compareResourceEPE("enums/integerEnum.e");
		}

		[Test]
		public void testTextEnum()
		{
			compareResourceEPE("enums/textEnum.e");
		}

	}
}

