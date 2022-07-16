using NUnit.Framework;
using prompto.parser;
using prompto.utils;

namespace prompto.runtime.o
{

	[TestFixture]
	public class TestCategories : BaseOParserTest
	{

		[SetUp]
		public void before()
		{
			Out.init();
		}

		[TearDown]
		public void after()
		{
			Out.restore();
		}

		[Test]
		public void testAttributeConstructor()
		{
			CheckOutput("categories/attributeConstructor.poc");
		}

		[Test]
		public void testCopyFromAscendant()
		{
			CheckOutput("categories/copyFromAscendant.poc");
		}

		[Test]
		public void testCopyFromAscendantWithOverride()
		{
			CheckOutput("categories/copyFromAscendantWithOverride.poc");
		}

		[Test]
		public void testCopyFromDescendant()
		{
			CheckOutput("categories/copyFromDescendant.poc");
		}

		[Test]
		public void testCopyFromDescendantWithOverride()
		{
			CheckOutput("categories/copyFromDescendantWithOverride.poc");
		}

		[Test]
		public void testCopyFromDocument()
		{
			CheckOutput("categories/copyFromDocument.poc");
		}

		[Test]
		public void testCopyFromStored()
		{
			CheckOutput("categories/copyFromStored.poc");
		}

		[Test]
		public void testEmptyConstructor()
		{
			CheckOutput("categories/emptyConstructor.poc");
		}

		[Test]
		public void testEquals()
		{
			CheckOutput("categories/equals.poc");
		}

		[Test]
		public void testLiteralConstructor()
		{
			CheckOutput("categories/literalConstructor.poc");
		}

		[Test]
		public void testPopulateFalse()
		{
			CheckOutput("categories/populateFalse.poc");
		}

		[Test]
		public void testResourceAttribute()
		{
			CheckOutput("categories/resourceAttribute.poc");
		}

		[Test]
		public void testSynonymConstructor()
		{
			CheckOutput("categories/synonymConstructor.poc");
		}

		[Test]
		public void testValueConstructor()
		{
			CheckOutput("categories/valueConstructor.poc");
		}

	}
}

