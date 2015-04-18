using NUnit.Framework;
using presto.parser;

namespace presto.translate.oeo
{

	[TestFixture]
	public class TestEnums : BaseOParserTest
	{

		[Test]
		public void testCategoryEnum()
		{
			compareResourceOEO("enums/categoryEnum.poc");
		}

		[Test]
		public void testIntegerEnum()
		{
			compareResourceOEO("enums/integerEnum.poc");
		}

		[Test]
		public void testTextEnum()
		{
			compareResourceOEO("enums/textEnum.poc");
		}

	}
}
