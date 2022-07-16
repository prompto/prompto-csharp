using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.omo
{

	[TestFixture]
	public class TestCategories : BaseOParserTest
	{

		[Test]
		public void testAttributeConstructor()
		{
			compareResourceOMO("categories/attributeConstructor.poc");
		}

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
		public void testEmptyConstructor()
		{
			compareResourceOMO("categories/emptyConstructor.poc");
		}

		[Test]
		public void testEquals()
		{
			compareResourceOMO("categories/equals.poc");
		}

		[Test]
		public void testLiteralConstructor()
		{
			compareResourceOMO("categories/literalConstructor.poc");
		}

		[Test]
		public void testPopulateFalse()
		{
			compareResourceOMO("categories/populateFalse.poc");
		}

		[Test]
		public void testResourceAttribute()
		{
			compareResourceOMO("categories/resourceAttribute.poc");
		}

		[Test]
		public void testSynonymConstructor()
		{
			compareResourceOMO("categories/synonymConstructor.poc");
		}

		[Test]
		public void testValueConstructor()
		{
			compareResourceOMO("categories/valueConstructor.poc");
		}

	}
}

