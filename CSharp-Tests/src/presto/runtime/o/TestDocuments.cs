using NUnit.Framework;
using presto.parser;
using presto.utils;

namespace presto.runtime.o
{

	[TestFixture]
	public class TestDocuments : BaseOParserTest
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
		public void testDeepItem()
		{
			CheckOutput("documents/deepItem.poc");
		}

		[Test]
		public void testDeepVariable()
		{
			CheckOutput("documents/deepVariable.poc");
		}

		[Test]
		public void testItem()
		{
			CheckOutput("documents/item.poc");
		}

		[Test]
		public void testVariable()
		{
			CheckOutput("documents/variable.poc");
		}

	}
}
