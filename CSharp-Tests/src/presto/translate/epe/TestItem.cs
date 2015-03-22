using NUnit.Framework;
using presto.parser;

namespace presto.translate.epe
{

	[TestFixture]
	public class TestItem : BaseEParserTest
	{

		[Test]
		public void testItemDict()
		{
			compareResourceEPE("item/itemDict.e");
		}

		[Test]
		public void testItemList()
		{
			compareResourceEPE("item/itemList.e");
		}

		[Test]
		public void testItemRange()
		{
			compareResourceEPE("item/itemRange.e");
		}

		[Test]
		public void testItemSet()
		{
			compareResourceEPE("item/itemSet.e");
		}

		[Test]
		public void testItemText()
		{
			compareResourceEPE("item/itemText.e");
		}

	}
}

