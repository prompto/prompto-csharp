using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.eoe
{

	[TestFixture]
	public class TestAdd : BaseEParserTest
	{

		[Test]
		public void testAddCharacter()
		{
			compareResourceEOE("add/addCharacter.pec");
		}

		[Test]
		public void testAddCss()
		{
			compareResourceEOE("add/addCss.pec");
		}

		[Test]
		public void testAddDate()
		{
			compareResourceEOE("add/addDate.pec");
		}

		[Test]
		public void testAddDateTime()
		{
			compareResourceEOE("add/addDateTime.pec");
		}

		[Test]
		public void testAddDecimal()
		{
			compareResourceEOE("add/addDecimal.pec");
		}

		[Test]
		public void testAddDecimalEnum()
		{
			compareResourceEOE("add/addDecimalEnum.pec");
		}

		[Test]
		public void testAddDict()
		{
			compareResourceEOE("add/addDict.pec");
		}

		[Test]
		public void testAddInteger()
		{
			compareResourceEOE("add/addInteger.pec");
		}

		[Test]
		public void testAddIntegerEnum()
		{
			compareResourceEOE("add/addIntegerEnum.pec");
		}

		[Test]
		public void testAddList()
		{
			compareResourceEOE("add/addList.pec");
		}

		[Test]
		public void testAddPeriod()
		{
			compareResourceEOE("add/addPeriod.pec");
		}

		[Test]
		public void testAddSet()
		{
			compareResourceEOE("add/addSet.pec");
		}

		[Test]
		public void testAddTextCharacter()
		{
			compareResourceEOE("add/addTextCharacter.pec");
		}

		[Test]
		public void testAddTextDecimal()
		{
			compareResourceEOE("add/addTextDecimal.pec");
		}

		[Test]
		public void testAddTextEnum()
		{
			compareResourceEOE("add/addTextEnum.pec");
		}

		[Test]
		public void testAddTextInteger()
		{
			compareResourceEOE("add/addTextInteger.pec");
		}

		[Test]
		public void testAddTextText()
		{
			compareResourceEOE("add/addTextText.pec");
		}

		[Test]
		public void testAddTime()
		{
			compareResourceEOE("add/addTime.pec");
		}

		[Test]
		public void testAddTuple()
		{
			compareResourceEOE("add/addTuple.pec");
		}

	}
}

