using NUnit.Framework;
using presto.parser;

namespace presto.translate.ese
{

	[TestFixture]
	public class TestEnums : BaseEParserTest
	{

		[Test]
		public void testCategoryEnum()
		{
			compareResourceESE("enums/categoryEnum.pec");
		}

		[Test]
		public void testIntegerEnum()
		{
			compareResourceESE("enums/integerEnum.pec");
		}

		[Test]
		public void testTextEnum()
		{
			compareResourceESE("enums/textEnum.pec");
		}

	}
}

