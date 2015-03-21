using NUnit.Framework;
using presto.parser;

namespace presto.translate.oeo
{

	[TestFixture]
	public class TestItem : BaseOParserTest
	{

		[Test]
		public void testItemDict()
		{
			compareResourceOEO("item/itemDict.o");
		}

		[Test]
		public void testItemList()
		{
			compareResourceOEO("item/itemList.o");
		}

		[Test]
		public void testItemRange()
		{
			compareResourceOEO("item/itemRange.o");
		}

		[Test]
		public void testItemSet()
		{
			compareResourceOEO("item/itemSet.o");
		}

		[Test]
		public void testItemText()
		{
			compareResourceOEO("item/itemText.o");
		}

	}
}

