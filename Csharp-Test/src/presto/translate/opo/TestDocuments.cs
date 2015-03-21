using NUnit.Framework;
using presto.parser;

namespace presto.translate.opo
{

	[TestFixture]
	public class TestDocuments : BaseOParserTest
	{

		[Test]
		public void testDeepItem()
		{
			compareResourceOPO("documents/deepItem.o");
		}

		[Test]
		public void testDeepVariable()
		{
			compareResourceOPO("documents/deepVariable.o");
		}

		[Test]
		public void testItem()
		{
			compareResourceOPO("documents/item.o");
		}

		[Test]
		public void testVariable()
		{
			compareResourceOPO("documents/variable.o");
		}

	}
}

