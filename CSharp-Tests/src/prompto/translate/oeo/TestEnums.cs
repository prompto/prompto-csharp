// generated: 2015-07-05T23:01:01.238
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

	}
}

