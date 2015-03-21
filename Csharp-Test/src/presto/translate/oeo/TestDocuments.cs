using NUnit.Framework;
using presto.parser;

namespace presto.translate.oeo
{

	[TestFixture]
	public class TestDocuments : BaseOParserTest
	{

		[Test]
		public void testDeepItem()
		{
			compareResourceOEO("documents/deepItem.o");
		}

		[Test]
		public void testDeepVariable()
		{
			compareResourceOEO("documents/deepVariable.o");
		}

		[Test]
		public void testItem()
		{
			compareResourceOEO("documents/item.o");
		}

		[Test]
		public void testVariable()
		{
			compareResourceOEO("documents/variable.o");
		}

	}
}

