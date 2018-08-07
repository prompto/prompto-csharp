using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.omo
{

	[TestFixture]
	public class TestDocuments : BaseOParserTest
	{

		[Test]
		public void testDeepItem()
		{
			compareResourceOMO("documents/deepItem.poc");
		}

		[Test]
		public void testDeepMember()
		{
			compareResourceOMO("documents/deepMember.poc");
		}

		[Test]
		public void testItem()
		{
			compareResourceOMO("documents/item.poc");
		}

		[Test]
		public void testLiteral()
		{
			compareResourceOMO("documents/literal.poc");
		}

		[Test]
		public void testMember()
		{
			compareResourceOMO("documents/member.poc");
		}

	}
}

