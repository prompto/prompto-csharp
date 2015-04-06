using NUnit.Framework;
using presto.parser;

namespace presto.translate.oso
{

	[TestFixture]
	public class TestEquals : BaseOParserTest
	{

		[Test]
		public void testEqBoolean()
		{
			compareResourceOSO("equals/eqBoolean.poc");
		}

		[Test]
		public void testEqCharacter()
		{
			compareResourceOSO("equals/eqCharacter.poc");
		}

		[Test]
		public void testEqDate()
		{
			compareResourceOSO("equals/eqDate.poc");
		}

		[Test]
		public void testEqDateTime()
		{
			compareResourceOSO("equals/eqDateTime.poc");
		}

		[Test]
		public void testEqDecimal()
		{
			compareResourceOSO("equals/eqDecimal.poc");
		}

		[Test]
		public void testEqDict()
		{
			compareResourceOSO("equals/eqDict.poc");
		}

		[Test]
		public void testEqInteger()
		{
			compareResourceOSO("equals/eqInteger.poc");
		}

		[Test]
		public void testEqList()
		{
			compareResourceOSO("equals/eqList.poc");
		}

		[Test]
		public void testEqPeriod()
		{
			compareResourceOSO("equals/eqPeriod.poc");
		}

		[Test]
		public void testEqRange()
		{
			compareResourceOSO("equals/eqRange.poc");
		}

		[Test]
		public void testEqSet()
		{
			compareResourceOSO("equals/eqSet.poc");
		}

		[Test]
		public void testEqText()
		{
			compareResourceOSO("equals/eqText.poc");
		}

		[Test]
		public void testEqTime()
		{
			compareResourceOSO("equals/eqTime.poc");
		}

		[Test]
		public void testIsBoolean()
		{
			compareResourceOSO("equals/isBoolean.poc");
		}

		[Test]
		public void testIsInstance()
		{
			compareResourceOSO("equals/isInstance.poc");
		}

		[Test]
		public void testIsNotBoolean()
		{
			compareResourceOSO("equals/isNotBoolean.poc");
		}

		[Test]
		public void testIsNotInstance()
		{
			compareResourceOSO("equals/isNotInstance.poc");
		}

		[Test]
		public void testNeqBoolean()
		{
			compareResourceOSO("equals/neqBoolean.poc");
		}

		[Test]
		public void testNeqCharacter()
		{
			compareResourceOSO("equals/neqCharacter.poc");
		}

		[Test]
		public void testNeqDate()
		{
			compareResourceOSO("equals/neqDate.poc");
		}

		[Test]
		public void testNeqDateTime()
		{
			compareResourceOSO("equals/neqDateTime.poc");
		}

		[Test]
		public void testNeqDecimal()
		{
			compareResourceOSO("equals/neqDecimal.poc");
		}

		[Test]
		public void testNeqDict()
		{
			compareResourceOSO("equals/neqDict.poc");
		}

		[Test]
		public void testNeqInteger()
		{
			compareResourceOSO("equals/neqInteger.poc");
		}

		[Test]
		public void testNeqList()
		{
			compareResourceOSO("equals/neqList.poc");
		}

		[Test]
		public void testNeqPeriod()
		{
			compareResourceOSO("equals/neqPeriod.poc");
		}

		[Test]
		public void testNeqRange()
		{
			compareResourceOSO("equals/neqRange.poc");
		}

		[Test]
		public void testNeqSet()
		{
			compareResourceOSO("equals/neqSet.poc");
		}

		[Test]
		public void testNeqText()
		{
			compareResourceOSO("equals/neqText.poc");
		}

		[Test]
		public void testNeqTime()
		{
			compareResourceOSO("equals/neqTime.poc");
		}

		[Test]
		public void testReqText()
		{
			compareResourceOSO("equals/reqText.poc");
		}

	}
}

