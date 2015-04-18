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
			compareResourceEOE("enums/categoryEnum.pec");
		}

		[Test]
		public void testIntegerEnum()
		{
			compareResourceEOE("enums/integerEnum.pec");
		}

		[Test]
		public void testTextEnum()
		{
			compareResourceEOE("enums/textEnum.pec");
		}

	}
}
