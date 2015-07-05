// generated: 2015-07-05T23:01:01.289
using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.ese
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

