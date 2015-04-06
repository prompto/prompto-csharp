using NUnit.Framework;
using presto.parser;

namespace presto.translate.oso
{

	[TestFixture]
	public class TestItem : BaseOParserTest
	{

		[Test]
		public void testItemDict()
		{
			compareResourceOSO("item/itemDict.poc");
		}

		[Test]
		public void testItemList()
		{
			compareResourceOSO("item/itemList.poc");
		}

		[Test]
		public void testItemRange()
		{
			compareResourceOSO("item/itemRange.poc");
		}

		[Test]
		public void testItemSet()
		{
			compareResourceOSO("item/itemSet.poc");
		}

		[Test]
		public void testItemText()
		{
			compareResourceOSO("item/itemText.poc");
		}

	}
}

