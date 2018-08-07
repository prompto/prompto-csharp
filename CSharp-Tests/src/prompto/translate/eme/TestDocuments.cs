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
		public void testDeepMember()
		{
			compareResourceEME("documents/deepMember.pec");
		}

		[Test]
		public void testItem()
		{
			compareResourceEME("documents/item.pec");
		}

		[Test]
		public void testLiteral()
		{
			compareResourceEME("documents/literal.pec");
		}

		[Test]
		public void testMember()
		{
			compareResourceEME("documents/member.pec");
		}

		[Test]
		public void testNamedItem()
		{
			compareResourceEME("documents/namedItem.pec");
		}

	}
}

