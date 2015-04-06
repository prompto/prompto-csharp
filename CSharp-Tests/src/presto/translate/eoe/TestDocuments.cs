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
			compareResourceEOE("documents/deepItem.pec");
		}

		[Test]
		public void testDeepVariable()
		{
			compareResourceEOE("documents/deepVariable.pec");
		}

		[Test]
		public void testItem()
		{
			compareResourceEOE("documents/item.pec");
		}

		[Test]
		public void testVariable()
		{
			compareResourceEOE("documents/variable.pec");
		}

	}
}

