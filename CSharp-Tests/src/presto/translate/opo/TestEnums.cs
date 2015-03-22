using NUnit.Framework;
using presto.parser;

namespace presto.translate.opo
{

	[TestFixture]
	public class TestEnums : BaseOParserTest
	{

		[Test]
		public void testCategoryEnum()
		{
			compareResourceOPO("enums/categoryEnum.o");
		}

		[Test]
		public void testIntegerEnum()
		{
			compareResourceOPO("enums/integerEnum.o");
		}

		[Test]
		public void testTextEnum()
		{
			compareResourceOPO("enums/textEnum.o");
		}

	}
}

