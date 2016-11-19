using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.eme
{

	[TestFixture]
	public class TestAdd : BaseEParserTest
	{

		[Test]
		public void testAddCharacter()
		{
			compareResourceEME("add/addCharacter.pec");
		}

		[Test]
		public void testAddDate()
		{
			compareResourceEME("add/addDate.pec");
		}

		[Test]
		public void testAddDateTime()
		{
			compareResourceEME("add/addDateTime.pec");
		}

		[Test]
		public void testAddDecimal()
		{
			compareResourceEME("add/addDecimal.pec");
		}

		[Test]
		public void testAddDecimalEnum()
		{
			compareResourceEME("add/addDecimalEnum.pec");
		}

		[Test]
		public void testAddDict()
		{
			compareResourceEME("add/addDict.pec");
		}

		[Test]
		public void testAddInteger()
		{
			compareResourceEME("add/addInteger.pec");
		}

		[Test]
		public void testAddIntegerEnum()
		{
			compareResourceEME("add/addIntegerEnum.pec");
		}

		[Test]
		public void testAddList()
		{
			compareResourceEME("add/addList.pec");
		}

		[Test]
		public void testAddPeriod()
		{
			compareResourceEME("add/addPeriod.pec");
		}

		[Test]
		public void testAddSet()
		{
			compareResourceEME("add/addSet.pec");
		}

		[Test]
		public void testAddTextCharacter()
		{
			compareResourceEME("add/addTextCharacter.pec");
		}

		[Test]
		public void testAddTextDecimal()
		{
			compareResourceEME("add/addTextDecimal.pec");
		}

		[Test]
		public void testAddTextEnum()
		{
			compareResourceEME("add/addTextEnum.pec");
		}

		[Test]
		public void testAddTextInteger()
		{
			compareResourceEME("add/addTextInteger.pec");
		}

		[Test]
		public void testAddTextText()
		{
			compareResourceEME("add/addTextText.pec");
		}

		[Test]
		public void testAddTime()
		{
			compareResourceEME("add/addTime.pec");
		}

		[Test]
		public void testAddTuple()
		{
			compareResourceEME("add/addTuple.pec");
		}

	}
}

