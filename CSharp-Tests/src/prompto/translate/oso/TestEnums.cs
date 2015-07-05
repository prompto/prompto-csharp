// generated: 2015-07-05T23:01:01.239
using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.oso
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

