using NUnit.Framework;
using presto.parser;

namespace presto.translate.oso
{

	[TestFixture]
	public class TestEnums : BaseOParserTest
	{

		[Test]
		public void testCategoryEnum()
		{
			compareResourceOSO("enums/categoryEnum.poc");
		}

		[Test]
		public void testIntegerEnum()
		{
			compareResourceOSO("enums/integerEnum.poc");
		}

		[Test]
		public void testTextEnum()
		{
			compareResourceOSO("enums/textEnum.poc");
		}

	}
}

