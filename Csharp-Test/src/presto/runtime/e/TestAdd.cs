using NUnit.Framework;
using presto.parser;
using presto.utils;

namespace presto.runtime.e
{

	[TestFixture]
	public class TestAdd : BaseEParserTest
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
			CheckOutput("add/addCharacter.e");
		}

		[Test]
		public void testAddDate()
		{
			CheckOutput("add/addDate.e");
		}

		[Test]
		public void testAddDateTime()
		{
			CheckOutput("add/addDateTime.e");
		}

		[Test]
		public void testAddDecimal()
		{
			CheckOutput("add/addDecimal.e");
		}

		[Test]
		public void testAddDict()
		{
			CheckOutput("add/addDict.e");
		}

		[Test]
		public void testAddInteger()
		{
			CheckOutput("add/addInteger.e");
		}

		[Test]
		public void testAddList()
		{
			CheckOutput("add/addList.e");
		}

		[Test]
		public void testAddPeriod()
		{
			CheckOutput("add/addPeriod.e");
		}

		[Test]
		public void testAddSet()
		{
			CheckOutput("add/addSet.e");
		}

		[Test]
		public void testAddText()
		{
			CheckOutput("add/addText.e");
		}

		[Test]
		public void testAddTime()
		{
			CheckOutput("add/addTime.e");
		}

		[Test]
		public void testAddTuple()
		{
			CheckOutput("add/addTuple.e");
		}

	}
}

