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
			compareResourceOEO("categories/copyFromAscendant.o");
		}

		[Test]
		public void testCopyFromAscendantWithOverride()
		{
			compareResourceOEO("categories/copyFromAscendantWithOverride.o");
		}

		[Test]
		public void testCopyFromDescendant()
		{
			compareResourceOEO("categories/copyFromDescendant.o");
		}

		[Test]
		public void testCopyFromDescendantWithOverride()
		{
			compareResourceOEO("categories/copyFromDescendantWithOverride.o");
		}

	}
}

