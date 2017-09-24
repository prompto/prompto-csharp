using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.oeo
{

	[TestFixture]
	public class TestEquals : BaseOParserTest
	{

		[Test]
		public void testEqBoolean()
		{
			compareResourceOEO("equals/eqBoolean.poc");
		}

		[Test]
		public void testEqCharacter()
		{
			compareResourceOEO("equals/eqCharacter.poc");
		}

		[Test]
		public void testEqDate()
		{
			compareResourceOEO("equals/eqDate.poc");
		}

		[Test]
		public void testEqDateTime()
		{
			compareResourceOEO("equals/eqDateTime.poc");
		}

		[Test]
		public void testEqDecimal()
		{
			compareResourceOEO("equals/eqDecimal.poc");
		}

		[Test]
		public void testEqDict()
		{
			compareResourceOEO("equals/eqDict.poc");
		}

		[Test]
		public void testEqInteger()
		{
			compareResourceOEO("equals/eqInteger.poc");
		}

		[Test]
		public void testEqList()
		{
			compareResourceOEO("equals/eqList.poc");
		}

		[Test]
		public void testEqPeriod()
		{
			compareResourceOEO("equals/eqPeriod.poc");
		}

		[Test]
		public void testEqRange()
		{
			compareResourceOEO("equals/eqRange.poc");
		}

		[Test]
		public void testEqSet()
		{
			compareResourceOEO("equals/eqSet.poc");
		}

		[Test]
		public void testEqText()
		{
			compareResourceOEO("equals/eqText.poc");
		}

		[Test]
		public void testEqTime()
		{
			compareResourceOEO("equals/eqTime.poc");
		}

		[Test]
		public void testEqVersion()
		{
			compareResourceOEO("equals/eqVersion.poc");
		}

		[Test]
		public void testIsBoolean()
		{
			compareResourceOEO("equals/isBoolean.poc");
		}

		[Test]
		public void testIsInstance()
		{
			compareResourceOEO("equals/isInstance.poc");
		}

		[Test]
		public void testIsNotBoolean()
		{
			compareResourceOEO("equals/isNotBoolean.poc");
		}

		[Test]
		public void testIsNotInstance()
		{
			compareResourceOEO("equals/isNotInstance.poc");
		}

		[Test]
		public void testNeqBoolean()
		{
			compareResourceOEO("equals/neqBoolean.poc");
		}

		[Test]
		public void testNeqCharacter()
		{
			compareResourceOEO("equals/neqCharacter.poc");
		}

		[Test]
		public void testNeqDate()
		{
			compareResourceOEO("equals/neqDate.poc");
		}

		[Test]
		public void testNeqDateTime()
		{
			compareResourceOEO("equals/neqDateTime.poc");
		}

		[Test]
		public void testNeqDecimal()
		{
			compareResourceOEO("equals/neqDecimal.poc");
		}

		[Test]
		public void testNeqDict()
		{
			compareResourceOEO("equals/neqDict.poc");
		}

		[Test]
		public void testNeqInteger()
		{
			compareResourceOEO("equals/neqInteger.poc");
		}

		[Test]
		public void testNeqList()
		{
			compareResourceOEO("equals/neqList.poc");
		}

		[Test]
		public void testNeqPeriod()
		{
			compareResourceOEO("equals/neqPeriod.poc");
		}

		[Test]
		public void testNeqRange()
		{
			compareResourceOEO("equals/neqRange.poc");
		}

		[Test]
		public void testNeqSet()
		{
			compareResourceOEO("equals/neqSet.poc");
		}

		[Test]
		public void testNeqText()
		{
			compareResourceOEO("equals/neqText.poc");
		}

		[Test]
		public void testNeqTime()
		{
			compareResourceOEO("equals/neqTime.poc");
		}

		[Test]
		public void testReqText()
		{
			compareResourceOEO("equals/reqText.poc");
		}

	}
}

