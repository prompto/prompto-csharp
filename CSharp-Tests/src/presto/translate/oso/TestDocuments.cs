using NUnit.Framework;
using presto.parser;

namespace presto.translate.oso
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

