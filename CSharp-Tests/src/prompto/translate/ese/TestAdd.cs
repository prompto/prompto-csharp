using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.ese
{

	[TestFixture]
	public class TestAdd : BaseEParserTest
	{

		[Test]
		public void testAddCharacter()
		{
			compareResourceESE("add/addCharacter.pec");
		}

		[Test]
		public void testAddDate()
		{
			compareResourceESE("add/addDate.pec");
		}

		[Test]
		public void testAddDateTime()
		{
			compareResourceESE("add/addDateTime.pec");
		}

		[Test]
		public void testAddDecimal()
		{
			compareResourceESE("add/addDecimal.pec");
		}

		[Test]
		public void testAddDict()
		{
			compareResourceESE("add/addDict.pec");
		}

		[Test]
		public void testAddInteger()
		{
			compareResourceESE("add/addInteger.pec");
		}

		[Test]
		public void testAddList()
		{
			compareResourceESE("add/addList.pec");
		}

		[Test]
		public void testAddPeriod()
		{
			compareResourceESE("add/addPeriod.pec");
		}

		[Test]
		public void testAddSet()
		{
			compareResourceESE("add/addSet.pec");
		}

		[Test]
		public void testAddTextDecimal()
		{
			compareResourceESE("add/addTextDecimal.pec");
		}

		[Test]
		public void testAddTextInteger()
		{
			compareResourceESE("add/addTextInteger.pec");
		}

		[Test]
		public void testAddTextText()
		{
			compareResourceESE("add/addTextText.pec");
		}

		[Test]
		public void testAddTime()
		{
			compareResourceESE("add/addTime.pec");
		}

		[Test]
		public void testAddTuple()
		{
			compareResourceESE("add/addTuple.pec");
		}

	}
}

