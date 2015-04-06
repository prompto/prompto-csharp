using NUnit.Framework;
using presto.parser;

namespace presto.translate.oeo
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
		public void testDeepVariable()
		{
			compareResourceOEO("documents/deepVariable.poc");
		}

		[Test]
		public void testItem()
		{
			compareResourceOEO("documents/item.poc");
		}

		[Test]
		public void testVariable()
		{
			compareResourceOEO("documents/variable.poc");
		}

	}
}

