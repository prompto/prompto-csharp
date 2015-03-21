using NUnit.Framework;
using presto.parser;

namespace presto.translate.opo
{

	[TestFixture]
	public class TestCategories : BaseOParserTest
	{

		[Test]
		public void testCopyFromAscendant()
		{
			compareResourceOPO("categories/copyFromAscendant.o");
		}

		[Test]
		public void testCopyFromAscendantWithOverride()
		{
			compareResourceOPO("categories/copyFromAscendantWithOverride.o");
		}

		[Test]
		public void testCopyFromDescendant()
		{
			compareResourceOPO("categories/copyFromDescendant.o");
		}

		[Test]
		public void testCopyFromDescendantWithOverride()
		{
			compareResourceOPO("categories/copyFromDescendantWithOverride.o");
		}

	}
}

