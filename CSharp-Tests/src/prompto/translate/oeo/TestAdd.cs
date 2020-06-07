using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.oeo
{

	[TestFixture]
	public class TestAdd : BaseOParserTest
	{

		[Test]
		public void testAddCharacter()
		{
			compareResourceOEO("add/addCharacter.poc");
		}

		[Test]
		public void testAddDate()
		{
			compareResourceOEO("add/addDate.poc");
		}

		[Test]
		public void testAddDateTime()
		{
			compareResourceOEO("add/addDateTime.poc");
		}

		[Test]
		public void testAddDecimal()
		{
			compareResourceOEO("add/addDecimal.poc");
		}

		[Test]
		public void testAddDict()
		{
			compareResourceOEO("add/addDict.poc");
		}

		[Test]
		public void testAddDocument()
		{
			compareResourceOEO("add/addDocument.poc");
		}

		[Test]
		public void testAddInteger()
		{
			compareResourceOEO("add/addInteger.poc");
		}

		[Test]
		public void testAddList()
		{
			compareResourceOEO("add/addList.poc");
		}

		[Test]
		public void testAddListDerived()
		{
			compareResourceOEO("add/addListDerived.poc");
		}

		[Test]
		public void testAddPeriod()
		{
			compareResourceOEO("add/addPeriod.poc");
		}

		[Test]
		public void testAddSet()
		{
			compareResourceOEO("add/addSet.poc");
		}

		[Test]
		public void testAddTextCharacter()
		{
			compareResourceOEO("add/addTextCharacter.poc");
		}

		[Test]
		public void testAddTextDecimal()
		{
			compareResourceOEO("add/addTextDecimal.poc");
		}

		[Test]
		public void testAddTextInteger()
		{
			compareResourceOEO("add/addTextInteger.poc");
		}

		[Test]
		public void testAddTextText()
		{
			compareResourceOEO("add/addTextText.poc");
		}

		[Test]
		public void testAddTime()
		{
			compareResourceOEO("add/addTime.poc");
		}

		[Test]
		public void testAddTuple()
		{
			compareResourceOEO("add/addTuple.poc");
		}

	}
}

