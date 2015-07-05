// generated: 2015-07-05T23:01:01.190
using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.oso
{

	[TestFixture]
	public class TestCategories : BaseOParserTest
	{

		[Test]
		public void testCopyFromAscendant()
		{
			compareResourceOSO("categories/copyFromAscendant.poc");
		}

		[Test]
		public void testCopyFromAscendantWithOverride()
		{
			compareResourceOSO("categories/copyFromAscendantWithOverride.poc");
		}

		[Test]
		public void testCopyFromDescendant()
		{
			compareResourceOSO("categories/copyFromDescendant.poc");
		}

		[Test]
		public void testCopyFromDescendantWithOverride()
		{
			compareResourceOSO("categories/copyFromDescendantWithOverride.poc");
		}

	}
}

