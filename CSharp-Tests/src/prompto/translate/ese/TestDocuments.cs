// generated: 2015-07-05T23:01:01.228
using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.ese
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

