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
			CheckOutput("documents/deepItem.o");
		}

		[Test]
		public void testDeepVariable()
		{
			CheckOutput("documents/deepVariable.o");
		}

		[Test]
		public void testItem()
		{
			CheckOutput("documents/item.o");
		}

		[Test]
		public void testVariable()
		{
			CheckOutput("documents/variable.o");
		}

	}
}

