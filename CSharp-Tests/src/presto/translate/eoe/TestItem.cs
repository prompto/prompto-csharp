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
			compareResourceEOE("item/itemDict.pec");
		}

		[Test]
		public void testItemList()
		{
			compareResourceEOE("item/itemList.pec");
		}

		[Test]
		public void testItemRange()
		{
			compareResourceEOE("item/itemRange.pec");
		}

		[Test]
		public void testItemSet()
		{
			compareResourceEOE("item/itemSet.pec");
		}

		[Test]
		public void testItemText()
		{
			compareResourceEOE("item/itemText.pec");
		}

	}
}

