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
			compareResourceOEO("add/addCharacter.o");
		}

		[Test]
		public void testAddDate()
		{
			compareResourceOEO("add/addDate.o");
		}

		[Test]
		public void testAddDateTime()
		{
			compareResourceOEO("add/addDateTime.o");
		}

		[Test]
		public void testAddDecimal()
		{
			compareResourceOEO("add/addDecimal.o");
		}

		[Test]
		public void testAddDict()
		{
			compareResourceOEO("add/addDict.o");
		}

		[Test]
		public void testAddInteger()
		{
			compareResourceOEO("add/addInteger.o");
		}

		[Test]
		public void testAddList()
		{
			compareResourceOEO("add/addList.o");
		}

		[Test]
		public void testAddPeriod()
		{
			compareResourceOEO("add/addPeriod.o");
		}

		[Test]
		public void testAddSet()
		{
			compareResourceOEO("add/addSet.o");
		}

		[Test]
		public void testAddText()
		{
			compareResourceOEO("add/addText.o");
		}

		[Test]
		public void testAddTime()
		{
			compareResourceOEO("add/addTime.o");
		}

		[Test]
		public void testAddTuple()
		{
			compareResourceOEO("add/addTuple.o");
		}

	}
}

