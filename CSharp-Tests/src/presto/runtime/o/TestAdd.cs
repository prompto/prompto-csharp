using NUnit.Framework;
using presto.parser;
using presto.utils;

namespace presto.runtime.o
{

	[TestFixture]
	public class TestAdd : BaseOParserTest
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
		public void testAddCharacter()
		{
			CheckOutput("add/addCharacter.o");
		}

		[Test]
		public void testAddDate()
		{
			CheckOutput("add/addDate.o");
		}

		[Test]
		public void testAddDateTime()
		{
			CheckOutput("add/addDateTime.o");
		}

		[Test]
		public void testAddDecimal()
		{
			CheckOutput("add/addDecimal.o");
		}

		[Test]
		public void testAddDict()
		{
			CheckOutput("add/addDict.o");
		}

		[Test]
		public void testAddInteger()
		{
			CheckOutput("add/addInteger.o");
		}

		[Test]
		public void testAddList()
		{
			CheckOutput("add/addList.o");
		}

		[Test]
		public void testAddPeriod()
		{
			CheckOutput("add/addPeriod.o");
		}

		[Test]
		public void testAddSet()
		{
			CheckOutput("add/addSet.o");
		}

		[Test]
		public void testAddText()
		{
			CheckOutput("add/addText.o");
		}

		[Test]
		public void testAddTime()
		{
			CheckOutput("add/addTime.o");
		}

		[Test]
		public void testAddTuple()
		{
			CheckOutput("add/addTuple.o");
		}

	}
}

