// generated: 2015-07-05T23:01:01.168
using NUnit.Framework;
using prompto.parser;
using prompto.utils;

namespace prompto.runtime.o
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
			CheckOutput("add/addCharacter.poc");
		}

		[Test]
		public void testAddDate()
		{
			CheckOutput("add/addDate.poc");
		}

		[Test]
		public void testAddDateTime()
		{
			CheckOutput("add/addDateTime.poc");
		}

		[Test]
		public void testAddDecimal()
		{
			CheckOutput("add/addDecimal.poc");
		}

		[Test]
		public void testAddDict()
		{
			CheckOutput("add/addDict.poc");
		}

		[Test]
		public void testAddInteger()
		{
			CheckOutput("add/addInteger.poc");
		}

		[Test]
		public void testAddList()
		{
			CheckOutput("add/addList.poc");
		}

		[Test]
		public void testAddPeriod()
		{
			CheckOutput("add/addPeriod.poc");
		}

		[Test]
		public void testAddSet()
		{
			CheckOutput("add/addSet.poc");
		}

		[Test]
		public void testAddText()
		{
			CheckOutput("add/addText.poc");
		}

		[Test]
		public void testAddTime()
		{
			CheckOutput("add/addTime.poc");
		}

		[Test]
		public void testAddTuple()
		{
			CheckOutput("add/addTuple.poc");
		}

	}
}

