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
		public void testDeepVariable()
		{
			compareResourceOMO("documents/deepVariable.poc");
		}

		[Test]
		public void testItem()
		{
			compareResourceOMO("documents/item.poc");
		}

		[Test]
		public void testVariable()
		{
			compareResourceOMO("documents/variable.poc");
		}

	}
}

