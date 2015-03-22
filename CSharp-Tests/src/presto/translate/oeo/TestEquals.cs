using NUnit.Framework;
using presto.parser;

namespace presto.translate.oeo
{

	[TestFixture]
	public class TestEquals : BaseOParserTest
	{

		[Test]
		public void testEqBoolean()
		{
			compareResourceOEO("equals/eqBoolean.o");
		}

		[Test]
		public void testEqCharacter()
		{
			compareResourceOEO("equals/eqCharacter.o");
		}

		[Test]
		public void testEqDate()
		{
			compareResourceOEO("equals/eqDate.o");
		}

		[Test]
		public void testEqDateTime()
		{
			compareResourceOEO("equals/eqDateTime.o");
		}

		[Test]
		public void testEqDecimal()
		{
			compareResourceOEO("equals/eqDecimal.o");
		}

		[Test]
		public void testEqDict()
		{
			compareResourceOEO("equals/eqDict.o");
		}

		[Test]
		public void testEqInteger()
		{
			compareResourceOEO("equals/eqInteger.o");
		}

		[Test]
		public void testEqList()
		{
			compareResourceOEO("equals/eqList.o");
		}

		[Test]
		public void testEqPeriod()
		{
			compareResourceOEO("equals/eqPeriod.o");
		}

		[Test]
		public void testEqRange()
		{
			compareResourceOEO("equals/eqRange.o");
		}

		[Test]
		public void testEqSet()
		{
			compareResourceOEO("equals/eqSet.o");
		}

		[Test]
		public void testEqText()
		{
			compareResourceOEO("equals/eqText.o");
		}

		[Test]
		public void testEqTime()
		{
			compareResourceOEO("equals/eqTime.o");
		}

		[Test]
		public void testIsBoolean()
		{
			compareResourceOEO("equals/isBoolean.o");
		}

		[Test]
		public void testIsInstance()
		{
			compareResourceOEO("equals/isInstance.o");
		}

		[Test]
		public void testIsNotBoolean()
		{
			compareResourceOEO("equals/isNotBoolean.o");
		}

		[Test]
		public void testIsNotInstance()
		{
			compareResourceOEO("equals/isNotInstance.o");
		}

		[Test]
		public void testNeqBoolean()
		{
			compareResourceOEO("equals/neqBoolean.o");
		}

		[Test]
		public void testNeqCharacter()
		{
			compareResourceOEO("equals/neqCharacter.o");
		}

		[Test]
		public void testNeqDate()
		{
			compareResourceOEO("equals/neqDate.o");
		}

		[Test]
		public void testNeqDateTime()
		{
			compareResourceOEO("equals/neqDateTime.o");
		}

		[Test]
		public void testNeqDecimal()
		{
			compareResourceOEO("equals/neqDecimal.o");
		}

		[Test]
		public void testNeqDict()
		{
			compareResourceOEO("equals/neqDict.o");
		}

		[Test]
		public void testNeqInteger()
		{
			compareResourceOEO("equals/neqInteger.o");
		}

		[Test]
		public void testNeqList()
		{
			compareResourceOEO("equals/neqList.o");
		}

		[Test]
		public void testNeqPeriod()
		{
			compareResourceOEO("equals/neqPeriod.o");
		}

		[Test]
		public void testNeqRange()
		{
			compareResourceOEO("equals/neqRange.o");
		}

		[Test]
		public void testNeqSet()
		{
			compareResourceOEO("equals/neqSet.o");
		}

		[Test]
		public void testNeqText()
		{
			compareResourceOEO("equals/neqText.o");
		}

		[Test]
		public void testNeqTime()
		{
			compareResourceOEO("equals/neqTime.o");
		}

		[Test]
		public void testReqText()
		{
			compareResourceOEO("equals/reqText.o");
		}

	}
}

