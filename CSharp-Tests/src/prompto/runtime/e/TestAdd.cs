using NUnit.Framework;
using prompto.parser;
using prompto.utils;

namespace prompto.runtime.e
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
			CheckOutput("add/addCharacter.pec");
		}

		[Test]
		public void testAddCss()
		{
			CheckOutput("add/addCss.pec");
		}

		[Test]
		public void testAddDate()
		{
			CheckOutput("add/addDate.pec");
		}

		[Test]
		public void testAddDateTime()
		{
			CheckOutput("add/addDateTime.pec");
		}

		[Test]
		public void testAddDecimal()
		{
			CheckOutput("add/addDecimal.pec");
		}

		[Test]
		public void testAddDecimalEnum()
		{
			CheckOutput("add/addDecimalEnum.pec");
		}

		[Test]
		public void testAddDict()
		{
			CheckOutput("add/addDict.pec");
		}

		[Test]
		public void testAddInteger()
		{
			CheckOutput("add/addInteger.pec");
		}

		[Test]
		public void testAddIntegerEnum()
		{
			CheckOutput("add/addIntegerEnum.pec");
		}

		[Test]
		public void testAddList()
		{
			CheckOutput("add/addList.pec");
		}

		[Test]
		public void testAddPeriod()
		{
			CheckOutput("add/addPeriod.pec");
		}

		[Test]
		public void testAddSet()
		{
			CheckOutput("add/addSet.pec");
		}

		[Test]
		public void testAddTextCharacter()
		{
			CheckOutput("add/addTextCharacter.pec");
		}

		[Test]
		public void testAddTextDecimal()
		{
			CheckOutput("add/addTextDecimal.pec");
		}

		[Test]
		public void testAddTextEnum()
		{
			CheckOutput("add/addTextEnum.pec");
		}

		[Test]
		public void testAddTextInteger()
		{
			CheckOutput("add/addTextInteger.pec");
		}

		[Test]
		public void testAddTextText()
		{
			CheckOutput("add/addTextText.pec");
		}

		[Test]
		public void testAddTime()
		{
			CheckOutput("add/addTime.pec");
		}

		[Test]
		public void testAddTuple()
		{
			CheckOutput("add/addTuple.pec");
		}

	}
}

