using NUnit.Framework;
using presto.parser;

namespace presto.translate.epe
{

	[TestFixture]
	public class TestAdd : BaseEParserTest
	{

		[Test]
		public void testAddCharacter()
		{
			compareResourceEPE("add/addCharacter.e");
		}

		[Test]
		public void testAddDate()
		{
			compareResourceEPE("add/addDate.e");
		}

		[Test]
		public void testAddDateTime()
		{
			compareResourceEPE("add/addDateTime.e");
		}

		[Test]
		public void testAddDecimal()
		{
			compareResourceEPE("add/addDecimal.e");
		}

		[Test]
		public void testAddDict()
		{
			compareResourceEPE("add/addDict.e");
		}

		[Test]
		public void testAddInteger()
		{
			compareResourceEPE("add/addInteger.e");
		}

		[Test]
		public void testAddList()
		{
			compareResourceEPE("add/addList.e");
		}

		[Test]
		public void testAddPeriod()
		{
			compareResourceEPE("add/addPeriod.e");
		}

		[Test]
		public void testAddSet()
		{
			compareResourceEPE("add/addSet.e");
		}

		[Test]
		public void testAddText()
		{
			compareResourceEPE("add/addText.e");
		}

		[Test]
		public void testAddTime()
		{
			compareResourceEPE("add/addTime.e");
		}

		[Test]
		public void testAddTuple()
		{
			compareResourceEPE("add/addTuple.e");
		}

	}
}

