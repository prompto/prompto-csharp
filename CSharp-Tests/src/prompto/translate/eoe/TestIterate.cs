using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.eoe
{

	[TestFixture]
	public class TestIterate : BaseEParserTest
	{

		[Test]
		public void testForEachCategoryList()
		{
			compareResourceEOE("iterate/forEachCategoryList.pec");
		}

		[Test]
		public void testForEachExpression()
		{
			compareResourceEOE("iterate/forEachExpression.pec");
		}

		[Test]
		public void testForEachIntegerList()
		{
			compareResourceEOE("iterate/forEachIntegerList.pec");
		}

		[Test]
		public void testForEachIntegerSet()
		{
			compareResourceEOE("iterate/forEachIntegerSet.pec");
		}

	}
}

