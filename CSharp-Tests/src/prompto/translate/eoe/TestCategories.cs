// generated: 2015-07-05T23:01:01.185
using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.eoe
{

	[TestFixture]
	public class TestCategories : BaseEParserTest
	{

		[Test]
		public void testCopyFromAscendant()
		{
			compareResourceEOE("categories/copyFromAscendant.pec");
		}

		[Test]
		public void testCopyFromAscendantWithOverride()
		{
			compareResourceEOE("categories/copyFromAscendantWithOverride.pec");
		}

		[Test]
		public void testCopyFromDescendant()
		{
			compareResourceEOE("categories/copyFromDescendant.pec");
		}

		[Test]
		public void testCopyFromDescendantWithOverride()
		{
			compareResourceEOE("categories/copyFromDescendantWithOverride.pec");
		}

	}
}

