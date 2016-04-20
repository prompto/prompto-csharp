using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.eoe
{

	[TestFixture]
	public class TestDocuments : BaseEParserTest
	{

		[Test]
		public void testBlob()
		{
			compareResourceEOE("documents/blob.pec");
		}

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

