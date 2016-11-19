using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.eme
{

	[TestFixture]
	public class TestItem : BaseEParserTest
	{

		[Test]
		public void testItemDict()
		{
			compareResourceEME("item/itemDict.pec");
		}

		[Test]
		public void testItemList()
		{
			compareResourceEME("item/itemList.pec");
		}

		[Test]
		public void testItemRange()
		{
			compareResourceEME("item/itemRange.pec");
		}

		[Test]
		public void testItemSet()
		{
			compareResourceEME("item/itemSet.pec");
		}

		[Test]
		public void testItemText()
		{
			compareResourceEME("item/itemText.pec");
		}

	}
}

