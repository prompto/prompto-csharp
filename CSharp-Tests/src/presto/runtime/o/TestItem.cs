using NUnit.Framework;
using presto.parser;
using presto.utils;

namespace presto.runtime.o
{

	[TestFixture]
	public class TestItem : BaseOParserTest
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
			CheckOutput("item/itemDict.poc");
		}

		[Test]
		public void testItemList()
		{
			CheckOutput("item/itemList.poc");
		}

		[Test]
		public void testItemRange()
		{
			CheckOutput("item/itemRange.poc");
		}

		[Test]
		public void testItemSet()
		{
			CheckOutput("item/itemSet.poc");
		}

		[Test]
		public void testItemText()
		{
			CheckOutput("item/itemText.poc");
		}

	}
}

