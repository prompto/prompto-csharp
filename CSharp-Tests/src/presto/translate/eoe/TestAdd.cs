using NUnit.Framework;
using presto.parser;

namespace presto.translate.eoe
{

	[TestFixture]
	public class TestAdd : BaseEParserTest
	{

		[Test]
		public void testAddCharacter()
		{
			compareResourceEOE("add/addCharacter.e");
		}

		[Test]
		public void testAddDate()
		{
			compareResourceEOE("add/addDate.e");
		}

		[Test]
		public void testAddDateTime()
		{
			compareResourceEOE("add/addDateTime.e");
		}

		[Test]
		public void testAddDecimal()
		{
			compareResourceEOE("add/addDecimal.e");
		}

		[Test]
		public void testAddDict()
		{
			compareResourceEOE("add/addDict.e");
		}

		[Test]
		public void testAddInteger()
		{
			compareResourceEOE("add/addInteger.e");
		}

		[Test]
		public void testAddList()
		{
			compareResourceEOE("add/addList.e");
		}

		[Test]
		public void testAddPeriod()
		{
			compareResourceEOE("add/addPeriod.e");
		}

		[Test]
		public void testAddSet()
		{
			compareResourceEOE("add/addSet.e");
		}

		[Test]
		public void testAddText()
		{
			compareResourceEOE("add/addText.e");
		}

		[Test]
		public void testAddTime()
		{
			compareResourceEOE("add/addTime.e");
		}

		[Test]
		public void testAddTuple()
		{
			compareResourceEOE("add/addTuple.e");
		}

	}
}

