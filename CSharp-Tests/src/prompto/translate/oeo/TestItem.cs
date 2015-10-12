using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.oeo
{

	[TestFixture]
	public class TestItem : BaseOParserTest
	{

		[Test]
		public void testItemDict()
		{
			compareResourceOEO("item/itemDict.poc");
		}

		[Test]
		public void testItemList()
		{
			compareResourceOEO("item/itemList.poc");
		}

		[Test]
		public void testItemRange()
		{
			compareResourceOEO("item/itemRange.poc");
		}

		[Test]
		public void testItemSet()
		{
			compareResourceOEO("item/itemSet.poc");
		}

		[Test]
		public void testItemText()
		{
			compareResourceOEO("item/itemText.poc");
		}

	}
}

