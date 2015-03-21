using NUnit.Framework;
using presto.parser;

namespace presto.translate.opo
{

	[TestFixture]
	public class TestAdd : BaseOParserTest
	{

		[Test]
		public void testAddCharacter()
		{
			compareResourceOPO("add/addCharacter.o");
		}

		[Test]
		public void testAddDate()
		{
			compareResourceOPO("add/addDate.o");
		}

		[Test]
		public void testAddDateTime()
		{
			compareResourceOPO("add/addDateTime.o");
		}

		[Test]
		public void testAddDecimal()
		{
			compareResourceOPO("add/addDecimal.o");
		}

		[Test]
		public void testAddDict()
		{
			compareResourceOPO("add/addDict.o");
		}

		[Test]
		public void testAddInteger()
		{
			compareResourceOPO("add/addInteger.o");
		}

		[Test]
		public void testAddList()
		{
			compareResourceOPO("add/addList.o");
		}

		[Test]
		public void testAddPeriod()
		{
			compareResourceOPO("add/addPeriod.o");
		}

		[Test]
		public void testAddSet()
		{
			compareResourceOPO("add/addSet.o");
		}

		[Test]
		public void testAddText()
		{
			compareResourceOPO("add/addText.o");
		}

		[Test]
		public void testAddTime()
		{
			compareResourceOPO("add/addTime.o");
		}

		[Test]
		public void testAddTuple()
		{
			compareResourceOPO("add/addTuple.o");
		}

	}
}

