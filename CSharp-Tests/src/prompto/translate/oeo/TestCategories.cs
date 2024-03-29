using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.oeo
{

	[TestFixture]
	public class TestCategories : BaseOParserTest
	{

		[Test]
		public void testAttributeConstructor()
		{
			compareResourceOEO("categories/attributeConstructor.poc");
		}

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

		[Test]
		public void testCopyFromDocument()
		{
			compareResourceOEO("categories/copyFromDocument.poc");
		}

		[Test]
		public void testCopyFromStored()
		{
			compareResourceOEO("categories/copyFromStored.poc");
		}

		[Test]
		public void testEmptyConstructor()
		{
			compareResourceOEO("categories/emptyConstructor.poc");
		}

		[Test]
		public void testEquals()
		{
			compareResourceOEO("categories/equals.poc");
		}

		[Test]
		public void testLiteralConstructor()
		{
			compareResourceOEO("categories/literalConstructor.poc");
		}

		[Test]
		public void testPopulateFalse()
		{
			compareResourceOEO("categories/populateFalse.poc");
		}

		[Test]
		public void testResourceAttribute()
		{
			compareResourceOEO("categories/resourceAttribute.poc");
		}

		[Test]
		public void testSynonymConstructor()
		{
			compareResourceOEO("categories/synonymConstructor.poc");
		}

		[Test]
		public void testValueConstructor()
		{
			compareResourceOEO("categories/valueConstructor.poc");
		}

	}
}

