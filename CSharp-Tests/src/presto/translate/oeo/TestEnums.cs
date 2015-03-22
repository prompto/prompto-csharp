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
			compareResourceOEO("enums/categoryEnum.o");
		}

		[Test]
		public void testIntegerEnum()
		{
			compareResourceOEO("enums/integerEnum.o");
		}

		[Test]
		public void testTextEnum()
		{
			compareResourceOEO("enums/textEnum.o");
		}

	}
}

