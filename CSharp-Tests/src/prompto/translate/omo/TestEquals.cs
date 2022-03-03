using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.omo
{

	[TestFixture]
	public class TestEquals : BaseOParserTest
	{

		[Test]
		public void testEqBoolean()
		{
			compareResourceOMO("equals/eqBoolean.poc");
		}

		[Test]
		public void testEqCharacter()
		{
			compareResourceOMO("equals/eqCharacter.poc");
		}

		[Test]
		public void testEqDate()
		{
			compareResourceOMO("equals/eqDate.poc");
		}

		[Test]
		public void testEqDateTime()
		{
			compareResourceOMO("equals/eqDateTime.poc");
		}

		[Test]
		public void testEqDecimal()
		{
			compareResourceOMO("equals/eqDecimal.poc");
		}

		[Test]
		public void testEqDict()
		{
			compareResourceOMO("equals/eqDict.poc");
		}

		[Test]
		public void testEqInteger()
		{
			compareResourceOMO("equals/eqInteger.poc");
		}

		[Test]
		public void testEqList()
		{
			compareResourceOMO("equals/eqList.poc");
		}

		[Test]
		public void testEqPeriod()
		{
			compareResourceOMO("equals/eqPeriod.poc");
		}

		[Test]
		public void testEqRange()
		{
			compareResourceOMO("equals/eqRange.poc");
		}

		[Test]
		public void testEqSet()
		{
			compareResourceOMO("equals/eqSet.poc");
		}

		[Test]
		public void testEqText()
		{
			compareResourceOMO("equals/eqText.poc");
		}

		[Test]
		public void testEqTime()
		{
			compareResourceOMO("equals/eqTime.poc");
		}

		[Test]
		public void testEqVersion()
		{
			compareResourceOMO("equals/eqVersion.poc");
		}

		[Test]
		public void testIsABoolean()
		{
			compareResourceOMO("equals/isABoolean.poc");
		}

		[Test]
		public void testIsADictionary()
		{
			compareResourceOMO("equals/isADictionary.poc");
		}

		[Test]
		public void testIsAParentInstance()
		{
			compareResourceOMO("equals/isAParentInstance.poc");
		}

		[Test]
		public void testIsAnInstance()
		{
			compareResourceOMO("equals/isAnInstance.poc");
		}

		[Test]
		public void testIsBoolean()
		{
			compareResourceOMO("equals/isBoolean.poc");
		}

		[Test]
		public void testIsInstance()
		{
			compareResourceOMO("equals/isInstance.poc");
		}

		[Test]
		public void testIsNotABoolean()
		{
			compareResourceOMO("equals/isNotABoolean.poc");
		}

		[Test]
		public void testIsNotBoolean()
		{
			compareResourceOMO("equals/isNotBoolean.poc");
		}

		[Test]
		public void testIsNotInstance()
		{
			compareResourceOMO("equals/isNotInstance.poc");
		}

		[Test]
		public void testNeqBoolean()
		{
			compareResourceOMO("equals/neqBoolean.poc");
		}

		[Test]
		public void testNeqCharacter()
		{
			compareResourceOMO("equals/neqCharacter.poc");
		}

		[Test]
		public void testNeqDate()
		{
			compareResourceOMO("equals/neqDate.poc");
		}

		[Test]
		public void testNeqDateTime()
		{
			compareResourceOMO("equals/neqDateTime.poc");
		}

		[Test]
		public void testNeqDecimal()
		{
			compareResourceOMO("equals/neqDecimal.poc");
		}

		[Test]
		public void testNeqDict()
		{
			compareResourceOMO("equals/neqDict.poc");
		}

		[Test]
		public void testNeqInteger()
		{
			compareResourceOMO("equals/neqInteger.poc");
		}

		[Test]
		public void testNeqList()
		{
			compareResourceOMO("equals/neqList.poc");
		}

		[Test]
		public void testNeqPeriod()
		{
			compareResourceOMO("equals/neqPeriod.poc");
		}

		[Test]
		public void testNeqRange()
		{
			compareResourceOMO("equals/neqRange.poc");
		}

		[Test]
		public void testNeqSet()
		{
			compareResourceOMO("equals/neqSet.poc");
		}

		[Test]
		public void testNeqText()
		{
			compareResourceOMO("equals/neqText.poc");
		}

		[Test]
		public void testNeqTime()
		{
			compareResourceOMO("equals/neqTime.poc");
		}

		[Test]
		public void testReqText()
		{
			compareResourceOMO("equals/reqText.poc");
		}

	}
}

