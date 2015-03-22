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
			CheckOutput("categories/copyFromAscendant.o");
		}

		[Test]
		public void testCopyFromAscendantWithOverride()
		{
			CheckOutput("categories/copyFromAscendantWithOverride.o");
		}

		[Test]
		public void testCopyFromDescendant()
		{
			CheckOutput("categories/copyFromDescendant.o");
		}

		[Test]
		public void testCopyFromDescendantWithOverride()
		{
			CheckOutput("categories/copyFromDescendantWithOverride.o");
		}

	}
}

