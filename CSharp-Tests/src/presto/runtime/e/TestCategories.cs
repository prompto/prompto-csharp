using NUnit.Framework;
using presto.parser;
using presto.utils;

namespace presto.runtime.e
{

	[TestFixture]
	public class TestCategories : BaseEParserTest
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
			CheckOutput("categories/copyFromAscendant.pec");
		}

		[Test]
		public void testCopyFromAscendantWithOverride()
		{
			CheckOutput("categories/copyFromAscendantWithOverride.pec");
		}

		[Test]
		public void testCopyFromDescendant()
		{
			CheckOutput("categories/copyFromDescendant.pec");
		}

		[Test]
		public void testCopyFromDescendantWithOverride()
		{
			CheckOutput("categories/copyFromDescendantWithOverride.pec");
		}

	}
}

