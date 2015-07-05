// generated: 2015-07-05T23:01:01.234
using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.eoe
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

