using NUnit.Framework;
using presto.parser;

namespace presto.translate.eoe
{

	[TestFixture]
	public class TestDocuments : BaseEParserTest
	{

		[Test]
		public void testDeepItem()
		{
			compareResourceEOE("documents/deepItem.e");
		}

		[Test]
		public void testDeepVariable()
		{
			compareResourceEOE("documents/deepVariable.e");
		}

		[Test]
		public void testItem()
		{
			compareResourceEOE("documents/item.e");
		}

		[Test]
		public void testVariable()
		{
			compareResourceEOE("documents/variable.e");
		}

	}
}

