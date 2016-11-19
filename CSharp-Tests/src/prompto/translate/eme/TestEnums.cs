using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.eme
{

	[TestFixture]
	public class TestEnums : BaseEParserTest
	{

		[Test]
		public void testCategoryEnum()
		{
			compareResourceEME("enums/categoryEnum.pec");
		}

		[Test]
		public void testIntegerEnum()
		{
			compareResourceEME("enums/integerEnum.pec");
		}

		[Test]
		public void testTextEnum()
		{
			compareResourceEME("enums/textEnum.pec");
		}

	}
}

