using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.omo
{

	[TestFixture]
	public class TestItem : BaseOParserTest
	{

		[Test]
		public void testItemDict()
		{
			compareResourceOMO("item/itemDict.poc");
		}

		[Test]
		public void testItemList()
		{
			compareResourceOMO("item/itemList.poc");
		}

		[Test]
		public void testItemRange()
		{
			compareResourceOMO("item/itemRange.poc");
		}

		[Test]
		public void testItemSet()
		{
			compareResourceOMO("item/itemSet.poc");
		}

		[Test]
		public void testItemText()
		{
			compareResourceOMO("item/itemText.poc");
		}

	}
}

