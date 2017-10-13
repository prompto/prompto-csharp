using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.omo
{

	[TestFixture]
	public class TestEnums : BaseOParserTest
	{

		[Test]
		public void testCategoryEnum()
		{
			compareResourceOMO("enums/categoryEnum.poc");
		}

		[Test]
		public void testIntegerEnum()
		{
			compareResourceOMO("enums/integerEnum.poc");
		}

		[Test]
		public void testTextEnum()
		{
			compareResourceOMO("enums/textEnum.poc");
		}

		[Test]
		public void testTextEnumArg()
		{
			compareResourceOMO("enums/textEnumArg.poc");
		}

		[Test]
		public void testTextEnumVar()
		{
			compareResourceOMO("enums/textEnumVar.poc");
		}

	}
}

