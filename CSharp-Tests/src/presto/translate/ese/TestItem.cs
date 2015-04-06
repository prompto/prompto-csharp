using NUnit.Framework;
using presto.parser;

namespace presto.translate.ese
{

	[TestFixture]
	public class TestItem : BaseEParserTest
	{

		[Test]
		public void testItemDict()
		{
			compareResourceESE("item/itemDict.pec");
		}

		[Test]
		public void testItemList()
		{
			compareResourceESE("item/itemList.pec");
		}

		[Test]
		public void testItemRange()
		{
			compareResourceESE("item/itemRange.pec");
		}

		[Test]
		public void testItemSet()
		{
			compareResourceESE("item/itemSet.pec");
		}

		[Test]
		public void testItemText()
		{
			compareResourceESE("item/itemText.pec");
		}

	}
}

