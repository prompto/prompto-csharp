using NUnit.Framework;
using presto.parser;

namespace presto.translate.eoe
{

	[TestFixture]
	public class TestEnums : BaseEParserTest
	{

		[Test]
		public void testCategoryEnum()
		{
			compareResourceEOE("enums/categoryEnum.e");
		}

		[Test]
		public void testIntegerEnum()
		{
			compareResourceEOE("enums/integerEnum.e");
		}

		[Test]
		public void testTextEnum()
		{
			compareResourceEOE("enums/textEnum.e");
		}

	}
}

