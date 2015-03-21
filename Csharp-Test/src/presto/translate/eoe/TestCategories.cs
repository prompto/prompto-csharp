using NUnit.Framework;
using presto.parser;

namespace presto.translate.eoe
{

	[TestFixture]
	public class TestCategories : BaseEParserTest
	{

		[Test]
		public void testCopyFromAscendant()
		{
			compareResourceEOE("categories/copyFromAscendant.e");
		}

		[Test]
		public void testCopyFromAscendantWithOverride()
		{
			compareResourceEOE("categories/copyFromAscendantWithOverride.e");
		}

		[Test]
		public void testCopyFromDescendant()
		{
			compareResourceEOE("categories/copyFromDescendant.e");
		}

		[Test]
		public void testCopyFromDescendantWithOverride()
		{
			compareResourceEOE("categories/copyFromDescendantWithOverride.e");
		}

	}
}

