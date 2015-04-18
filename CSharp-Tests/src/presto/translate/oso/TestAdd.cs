using NUnit.Framework;
using presto.parser;

namespace presto.translate.oso
{

	[TestFixture]
	public class TestAdd : BaseOParserTest
	{

		[Test]
		public void testAddCharacter()
		{
			compareResourceOSO("add/addCharacter.poc");
		}

		[Test]
		public void testAddDate()
		{
			compareResourceOSO("add/addDate.poc");
		}

		[Test]
		public void testAddDateTime()
		{
			compareResourceOSO("add/addDateTime.poc");
		}

		[Test]
		public void testAddDecimal()
		{
			compareResourceOSO("add/addDecimal.poc");
		}

		[Test]
		public void testAddDict()
		{
			compareResourceOSO("add/addDict.poc");
		}

		[Test]
		public void testAddInteger()
		{
			compareResourceOSO("add/addInteger.poc");
		}

		[Test]
		public void testAddList()
		{
			compareResourceOSO("add/addList.poc");
		}

		[Test]
		public void testAddPeriod()
		{
			compareResourceOSO("add/addPeriod.poc");
		}

		[Test]
		public void testAddSet()
		{
			compareResourceOSO("add/addSet.poc");
		}

		[Test]
		public void testAddText()
		{
			compareResourceOSO("add/addText.poc");
		}

		[Test]
		public void testAddTime()
		{
			compareResourceOSO("add/addTime.poc");
		}

		[Test]
		public void testAddTuple()
		{
			compareResourceOSO("add/addTuple.poc");
		}

	}
}
