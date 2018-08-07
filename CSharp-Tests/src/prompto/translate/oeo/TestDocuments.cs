using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.oeo
{

	[TestFixture]
	public class TestDocuments : BaseOParserTest
	{

		[Test]
		public void testDeepItem()
		{
			compareResourceOEO("documents/deepItem.poc");
		}

		[Test]
		public void testDeepMember()
		{
			compareResourceOEO("documents/deepMember.poc");
		}

		[Test]
		public void testItem()
		{
			compareResourceOEO("documents/item.poc");
		}

		[Test]
		public void testLiteral()
		{
			compareResourceOEO("documents/literal.poc");
		}

		[Test]
		public void testMember()
		{
			compareResourceOEO("documents/member.poc");
		}

	}
}

