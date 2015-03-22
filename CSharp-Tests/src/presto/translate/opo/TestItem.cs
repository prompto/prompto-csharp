using NUnit.Framework;
using presto.parser;

namespace presto.translate.opo
{

	[TestFixture]
	public class TestItem : BaseOParserTest
	{

		[Test]
		public void testItemDict()
		{
			compareResourceOPO("item/itemDict.o");
		}

		[Test]
		public void testItemList()
		{
			compareResourceOPO("item/itemList.o");
		}

		[Test]
		public void testItemRange()
		{
			compareResourceOPO("item/itemRange.o");
		}

		[Test]
		public void testItemSet()
		{
			compareResourceOPO("item/itemSet.o");
		}

		[Test]
		public void testItemText()
		{
			compareResourceOPO("item/itemText.o");
		}

	}
}

