using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.ese
{

	[TestFixture]
	public class TestCategories : BaseEParserTest
	{

		[Test]
		public void testComposed()
		{
			compareResourceESE("categories/composed.pec");
		}

		[Test]
		public void testCopyFromAscendant()
		{
			compareResourceESE("categories/copyFromAscendant.pec");
		}

		[Test]
		public void testCopyFromAscendantWithOverride()
		{
			compareResourceESE("categories/copyFromAscendantWithOverride.pec");
		}

		[Test]
		public void testCopyFromDescendant()
		{
			compareResourceESE("categories/copyFromDescendant.pec");
		}

		[Test]
		public void testCopyFromDescendantWithOverride()
		{
			compareResourceESE("categories/copyFromDescendantWithOverride.pec");
		}

	}
}

