using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.omo
{

	[TestFixture]
	public class TestAdd : BaseOParserTest
	{

		[Test]
		public void testAddCharacter()
		{
			compareResourceOMO("add/addCharacter.poc");
		}

		[Test]
		public void testAddDate()
		{
			compareResourceOMO("add/addDate.poc");
		}

		[Test]
		public void testAddDateTime()
		{
			compareResourceOMO("add/addDateTime.poc");
		}

		[Test]
		public void testAddDecimal()
		{
			compareResourceOMO("add/addDecimal.poc");
		}

		[Test]
		public void testAddDict()
		{
			compareResourceOMO("add/addDict.poc");
		}

		[Test]
		public void testAddInteger()
		{
			compareResourceOMO("add/addInteger.poc");
		}

		[Test]
		public void testAddList()
		{
			compareResourceOMO("add/addList.poc");
		}

		[Test]
		public void testAddPeriod()
		{
			compareResourceOMO("add/addPeriod.poc");
		}

		[Test]
		public void testAddSet()
		{
			compareResourceOMO("add/addSet.poc");
		}

		[Test]
		public void testAddTextCharacter()
		{
			compareResourceOMO("add/addTextCharacter.poc");
		}

		[Test]
		public void testAddTextDecimal()
		{
			compareResourceOMO("add/addTextDecimal.poc");
		}

		[Test]
		public void testAddTextInteger()
		{
			compareResourceOMO("add/addTextInteger.poc");
		}

		[Test]
		public void testAddTextText()
		{
			compareResourceOMO("add/addTextText.poc");
		}

		[Test]
		public void testAddTime()
		{
			compareResourceOMO("add/addTime.poc");
		}

		[Test]
		public void testAddTuple()
		{
			compareResourceOMO("add/addTuple.poc");
		}

	}
}

