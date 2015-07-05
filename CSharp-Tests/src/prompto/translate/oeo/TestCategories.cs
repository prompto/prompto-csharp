// generated: 2015-07-05T23:01:01.188
using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.oeo
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

