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
			CheckOutput("item/itemDict.o");
		}

		[Test]
		public void testItemList()
		{
			CheckOutput("item/itemList.o");
		}

		[Test]
		public void testItemRange()
		{
			CheckOutput("item/itemRange.o");
		}

		[Test]
		public void testItemSet()
		{
			CheckOutput("item/itemSet.o");
		}

		[Test]
		public void testItemText()
		{
			CheckOutput("item/itemText.o");
		}

	}
}

