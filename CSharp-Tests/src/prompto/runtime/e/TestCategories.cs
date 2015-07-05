// generated: 2015-07-05T23:01:01.187
using NUnit.Framework;
using prompto.parser;
using prompto.utils;

namespace prompto.runtime.e
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

