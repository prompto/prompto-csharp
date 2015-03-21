using NUnit.Framework;
using presto.parser;

namespace presto.translate.epe
{

	[TestFixture]
	public class TestCategories : BaseEParserTest
	{

		[Test]
		public void testCopyFromAscendant()
		{
			compareResourceEPE("categories/copyFromAscendant.e");
		}

		[Test]
		public void testCopyFromAscendantWithOverride()
		{
			compareResourceEPE("categories/copyFromAscendantWithOverride.e");
		}

		[Test]
		public void testCopyFromDescendant()
		{
			compareResourceEPE("categories/copyFromDescendant.e");
		}

		[Test]
		public void testCopyFromDescendantWithOverride()
		{
			compareResourceEPE("categories/copyFromDescendantWithOverride.e");
		}

	}
}

