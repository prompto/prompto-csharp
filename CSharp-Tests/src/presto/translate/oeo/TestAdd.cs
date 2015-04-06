using NUnit.Framework;
using presto.parser;

namespace presto.translate.oeo
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
		public void testAddText()
		{
			compareResourceOEO("add/addText.poc");
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

