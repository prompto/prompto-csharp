using NUnit.Framework;
using presto.parser;
using presto.utils;

namespace presto.runtime.o
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

	}
}

