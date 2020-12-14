using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.omo
{

	[TestFixture]
	public class TestCategories : BaseOParserTest
	{

		[Test]
		public void testCopyFromAscendant()
		{
			compareResourceOMO("categories/copyFromAscendant.poc");
		}

		[Test]
		public void testCopyFromAscendantWithOverride()
		{
			compareResourceOMO("categories/copyFromAscendantWithOverride.poc");
		}

		[Test]
		public void testCopyFromDescendant()
		{
			compareResourceOMO("categories/copyFromDescendant.poc");
		}

		[Test]
		public void testCopyFromDescendantWithOverride()
		{
			compareResourceOMO("categories/copyFromDescendantWithOverride.poc");
		}

		[Test]
		public void testCopyFromDocument()
		{
			compareResourceOMO("categories/copyFromDocument.poc");
		}

		[Test]
		public void testCopyFromStored()
		{
			compareResourceOMO("categories/copyFromStored.poc");
		}

		[Test]
		public void testEquals()
		{
			compareResourceOMO("categories/equals.poc");
		}

		[Test]
		public void testPopulateFalse()
		{
			compareResourceOMO("categories/populateFalse.poc");
		}

	}
}

