using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.eme
{

	[TestFixture]
	public class TestCategories : BaseEParserTest
	{

		[Test]
		public void testComposed()
		{
			compareResourceEME("categories/composed.pec");
		}

		[Test]
		public void testCopyFromAscendant()
		{
			compareResourceEME("categories/copyFromAscendant.pec");
		}

		[Test]
		public void testCopyFromAscendantWithOverride()
		{
			compareResourceEME("categories/copyFromAscendantWithOverride.pec");
		}

		[Test]
		public void testCopyFromDescendant()
		{
			compareResourceEME("categories/copyFromDescendant.pec");
		}

		[Test]
		public void testCopyFromDescendantWithOverride()
		{
			compareResourceEME("categories/copyFromDescendantWithOverride.pec");
		}

		[Test]
		public void testCopyFromDocument()
		{
			compareResourceEME("categories/copyFromDocument.pec");
		}

	}
}

