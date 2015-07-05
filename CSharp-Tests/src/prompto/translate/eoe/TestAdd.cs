// generated: 2015-07-05T23:01:01.162
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
		public void testAddText()
		{
			compareResourceEOE("add/addText.pec");
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

