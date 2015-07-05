// generated: 2015-07-05T23:01:01.231
using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.oso
{

	[TestFixture]
	public class TestDocuments : BaseOParserTest
	{

		[Test]
		public void testDeepItem()
		{
			compareResourceOSO("documents/deepItem.poc");
		}

		[Test]
		public void testDeepVariable()
		{
			compareResourceOSO("documents/deepVariable.poc");
		}

		[Test]
		public void testItem()
		{
			compareResourceOSO("documents/item.poc");
		}

		[Test]
		public void testVariable()
		{
			compareResourceOSO("documents/variable.poc");
		}

	}
}

