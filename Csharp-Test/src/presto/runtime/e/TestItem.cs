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
			CheckOutput("item/itemDict.e");
		}

		[Test]
		public void testItemList()
		{
			CheckOutput("item/itemList.e");
		}

		[Test]
		public void testItemRange()
		{
			CheckOutput("item/itemRange.e");
		}

		[Test]
		public void testItemSet()
		{
			CheckOutput("item/itemSet.e");
		}

		[Test]
		public void testItemText()
		{
			CheckOutput("item/itemText.e");
		}

	}
}

