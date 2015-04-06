using NUnit.Framework;
using presto.parser;

namespace presto.translate.ese
{

	[TestFixture]
	public class TestDocuments : BaseEParserTest
	{

		[Test]
		public void testDeepItem()
		{
			compareResourceESE("documents/deepItem.pec");
		}

		[Test]
		public void testDeepVariable()
		{
			compareResourceESE("documents/deepVariable.pec");
		}

		[Test]
		public void testItem()
		{
			compareResourceESE("documents/item.pec");
		}

		[Test]
		public void testVariable()
		{
			compareResourceESE("documents/variable.pec");
		}

	}
}

