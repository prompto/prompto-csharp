using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.eme
{

	[TestFixture]
	public class TestDocuments : BaseEParserTest
	{

		[Test]
		public void testBlob()
		{
			compareResourceEME("documents/blob.pec");
		}

		[Test]
		public void testDeepItem()
		{
			compareResourceEME("documents/deepItem.pec");
		}

		[Test]
		public void testDeepVariable()
		{
			compareResourceEME("documents/deepVariable.pec");
		}

		[Test]
		public void testItem()
		{
			compareResourceEME("documents/item.pec");
		}

		[Test]
		public void testNamedItem()
		{
			compareResourceEME("documents/namedItem.pec");
		}

		[Test]
		public void testVariable()
		{
			compareResourceEME("documents/variable.pec");
		}

	}
}

