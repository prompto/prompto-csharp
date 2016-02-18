using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.ese
{

	[TestFixture]
	public class TestIterate : BaseEParserTest
	{

		[Test]
		public void testForEachCategoryList()
		{
			compareResourceESE("iterate/forEachCategoryList.pec");
		}

		[Test]
		public void testForEachIntegerList()
		{
			compareResourceESE("iterate/forEachIntegerList.pec");
		}

	}
}

