using NUnit.Framework;
using presto.parser;
using presto.utils;

namespace presto.runtime.e
{

	[TestFixture]
	public class TestItem : BaseEParserTest
	{

		[SetUp]
		public void before()
		{
			Out.init();
		}

		[TearDown]
		public void after()
		{
			Out.restore();
		}

		[Test]
		public void testItemDict()
		{
			CheckOutput("item/itemDict.pec");
		}

		[Test]
		public void testItemList()
		{
			CheckOutput("item/itemList.pec");
		}

		[Test]
		public void testItemRange()
		{
			CheckOutput("item/itemRange.pec");
		}

		[Test]
		public void testItemSet()
		{
			CheckOutput("item/itemSet.pec");
		}

		[Test]
		public void testItemText()
		{
			CheckOutput("item/itemText.pec");
		}

	}
}

