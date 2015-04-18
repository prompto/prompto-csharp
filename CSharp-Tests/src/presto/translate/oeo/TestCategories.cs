using NUnit.Framework;
using presto.parser;

namespace presto.translate.oeo
{

	[TestFixture]
	public class TestCategories : BaseOParserTest
	{

		[Test]
		public void testCopyFromAscendant()
		{
			compareResourceOEO("categories/copyFromAscendant.poc");
		}

		[Test]
		public void testCopyFromAscendantWithOverride()
		{
			compareResourceOEO("categories/copyFromAscendantWithOverride.poc");
		}

		[Test]
		public void testCopyFromDescendant()
		{
			compareResourceOEO("categories/copyFromDescendant.poc");
		}

		[Test]
		public void testCopyFromDescendantWithOverride()
		{
			compareResourceOEO("categories/copyFromDescendantWithOverride.poc");
		}

	}
}
