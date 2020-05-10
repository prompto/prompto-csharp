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
		public void testDeepMember()
		{
			compareResourceEOE("documents/deepMember.pec");
		}

		[Test]
		public void testInstance()
		{
			compareResourceEOE("documents/instance.pec");
		}

		[Test]
		public void testItem()
		{
			compareResourceEOE("documents/item.pec");
		}

		[Test]
		public void testLiteral()
		{
			compareResourceEOE("documents/literal.pec");
		}

		[Test]
		public void testMember()
		{
			compareResourceEOE("documents/member.pec");
		}

		[Test]
		public void testNamedItem()
		{
			compareResourceEOE("documents/namedItem.pec");
		}

	}
}

