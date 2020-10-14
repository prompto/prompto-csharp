using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.eme
{

	[TestFixture]
	public class TestIterate : BaseEParserTest
	{

		[Test]
		public void testForEachCategoryList()
		{
			compareResourceEME("iterate/forEachCategoryList.pec");
		}

		[Test]
		public void testForEachExpression()
		{
			compareResourceEME("iterate/forEachExpression.pec");
		}

		[Test]
		public void testForEachIntegerList()
		{
			compareResourceEME("iterate/forEachIntegerList.pec");
		}

		[Test]
		public void testForEachIntegerSet()
		{
			compareResourceEME("iterate/forEachIntegerSet.pec");
		}

	}
}

