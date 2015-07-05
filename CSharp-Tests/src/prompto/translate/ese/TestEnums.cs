// generated: 2015-07-05T23:01:01.236
using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.ese
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
