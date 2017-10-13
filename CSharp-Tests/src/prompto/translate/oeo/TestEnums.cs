using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.oeo
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

		[Test]
		public void testTextEnumArg()
		{
			compareResourceOEO("enums/textEnumArg.poc");
		}

		[Test]
		public void testTextEnumVar()
		{
			compareResourceOEO("enums/textEnumVar.poc");
		}

	}
}

