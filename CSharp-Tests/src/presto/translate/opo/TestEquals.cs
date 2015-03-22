using NUnit.Framework;
using presto.parser;

namespace presto.translate.opo
{

	[TestFixture]
	public class TestEquals : BaseOParserTest
	{

		[Test]
		public void testEqBoolean()
		{
			compareResourceOPO("equals/eqBoolean.o");
		}

		[Test]
		public void testEqCharacter()
		{
			compareResourceOPO("equals/eqCharacter.o");
		}

		[Test]
		public void testEqDate()
		{
			compareResourceOPO("equals/eqDate.o");
		}

		[Test]
		public void testEqDateTime()
		{
			compareResourceOPO("equals/eqDateTime.o");
		}

		[Test]
		public void testEqDecimal()
		{
			compareResourceOPO("equals/eqDecimal.o");
		}

		[Test]
		public void testEqDict()
		{
			compareResourceOPO("equals/eqDict.o");
		}

		[Test]
		public void testEqInteger()
		{
			compareResourceOPO("equals/eqInteger.o");
		}

		[Test]
		public void testEqList()
		{
			compareResourceOPO("equals/eqList.o");
		}

		[Test]
		public void testEqPeriod()
		{
			compareResourceOPO("equals/eqPeriod.o");
		}

		[Test]
		public void testEqRange()
		{
			compareResourceOPO("equals/eqRange.o");
		}

		[Test]
		public void testEqSet()
		{
			compareResourceOPO("equals/eqSet.o");
		}

		[Test]
		public void testEqText()
		{
			compareResourceOPO("equals/eqText.o");
		}

		[Test]
		public void testEqTime()
		{
			compareResourceOPO("equals/eqTime.o");
		}

		[Test]
		public void testIsBoolean()
		{
			compareResourceOPO("equals/isBoolean.o");
		}

		[Test]
		public void testIsInstance()
		{
			compareResourceOPO("equals/isInstance.o");
		}

		[Test]
		public void testIsNotBoolean()
		{
			compareResourceOPO("equals/isNotBoolean.o");
		}

		[Test]
		public void testIsNotInstance()
		{
			compareResourceOPO("equals/isNotInstance.o");
		}

		[Test]
		public void testNeqBoolean()
		{
			compareResourceOPO("equals/neqBoolean.o");
		}

		[Test]
		public void testNeqCharacter()
		{
			compareResourceOPO("equals/neqCharacter.o");
		}

		[Test]
		public void testNeqDate()
		{
			compareResourceOPO("equals/neqDate.o");
		}

		[Test]
		public void testNeqDateTime()
		{
			compareResourceOPO("equals/neqDateTime.o");
		}

		[Test]
		public void testNeqDecimal()
		{
			compareResourceOPO("equals/neqDecimal.o");
		}

		[Test]
		public void testNeqDict()
		{
			compareResourceOPO("equals/neqDict.o");
		}

		[Test]
		public void testNeqInteger()
		{
			compareResourceOPO("equals/neqInteger.o");
		}

		[Test]
		public void testNeqList()
		{
			compareResourceOPO("equals/neqList.o");
		}

		[Test]
		public void testNeqPeriod()
		{
			compareResourceOPO("equals/neqPeriod.o");
		}

		[Test]
		public void testNeqRange()
		{
			compareResourceOPO("equals/neqRange.o");
		}

		[Test]
		public void testNeqSet()
		{
			compareResourceOPO("equals/neqSet.o");
		}

		[Test]
		public void testNeqText()
		{
			compareResourceOPO("equals/neqText.o");
		}

		[Test]
		public void testNeqTime()
		{
			compareResourceOPO("equals/neqTime.o");
		}

		[Test]
		public void testReqText()
		{
			compareResourceOPO("equals/reqText.o");
		}

	}
}

