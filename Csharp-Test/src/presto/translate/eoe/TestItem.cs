using NUnit.Framework;
using presto.parser;

namespace presto.translate.eoe
{

	[TestFixture]
	public class TestItem : BaseEParserTest
	{

		[Test]
		public void testItemDict()
		{
			compareResourceEOE("item/itemDict.e");
		}

		[Test]
		public void testItemList()
		{
			compareResourceEOE("item/itemList.e");
		}

		[Test]
		public void testItemRange()
		{
			compareResourceEOE("item/itemRange.e");
		}

		[Test]
		public void testItemSet()
		{
			compareResourceEOE("item/itemSet.e");
		}

		[Test]
		public void testItemText()
		{
			compareResourceEOE("item/itemText.e");
		}

	}
}

