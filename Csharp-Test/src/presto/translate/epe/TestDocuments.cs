using NUnit.Framework;
using presto.parser;

namespace presto.translate.epe
{

	[TestFixture]
	public class TestDocuments : BaseEParserTest
	{

		[Test]
		public void testDeepItem()
		{
			compareResourceEPE("documents/deepItem.e");
		}

		[Test]
		public void testDeepVariable()
		{
			compareResourceEPE("documents/deepVariable.e");
		}

		[Test]
		public void testItem()
		{
			compareResourceEPE("documents/item.e");
		}

		[Test]
		public void testVariable()
		{
			compareResourceEPE("documents/variable.e");
		}

	}
}

