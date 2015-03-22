using NUnit.Framework;
using presto.parser;

namespace presto.translate.eoe
{

	[TestFixture]
	public class TestEquals : BaseEParserTest
	{

		[Test]
		public void testEqBoolean()
		{
			compareResourceEOE("equals/eqBoolean.e");
		}

		[Test]
		public void testEqCharacter()
		{
			compareResourceEOE("equals/eqCharacter.e");
		}

		[Test]
		public void testEqDate()
		{
			compareResourceEOE("equals/eqDate.e");
		}

		[Test]
		public void testEqDateTime()
		{
			compareResourceEOE("equals/eqDateTime.e");
		}

		[Test]
		public void testEqDecimal()
		{
			compareResourceEOE("equals/eqDecimal.e");
		}

		[Test]
		public void testEqDict()
		{
			compareResourceEOE("equals/eqDict.e");
		}

		[Test]
		public void testEqInteger()
		{
			compareResourceEOE("equals/eqInteger.e");
		}

		[Test]
		public void testEqList()
		{
			compareResourceEOE("equals/eqList.e");
		}

		[Test]
		public void testEqPeriod()
		{
			compareResourceEOE("equals/eqPeriod.e");
		}

		[Test]
		public void testEqRange()
		{
			compareResourceEOE("equals/eqRange.e");
		}

		[Test]
		public void testEqSet()
		{
			compareResourceEOE("equals/eqSet.e");
		}

		[Test]
		public void testEqText()
		{
			compareResourceEOE("equals/eqText.e");
		}

		[Test]
		public void testEqTime()
		{
			compareResourceEOE("equals/eqTime.e");
		}

		[Test]
		public void testIsBoolean()
		{
			compareResourceEOE("equals/isBoolean.e");
		}

		[Test]
		public void testIsInstance()
		{
			compareResourceEOE("equals/isInstance.e");
		}

		[Test]
		public void testIsNotBoolean()
		{
			compareResourceEOE("equals/isNotBoolean.e");
		}

		[Test]
		public void testIsNotInstance()
		{
			compareResourceEOE("equals/isNotInstance.e");
		}

		[Test]
		public void testNeqBoolean()
		{
			compareResourceEOE("equals/neqBoolean.e");
		}

		[Test]
		public void testNeqCharacter()
		{
			compareResourceEOE("equals/neqCharacter.e");
		}

		[Test]
		public void testNeqDate()
		{
			compareResourceEOE("equals/neqDate.e");
		}

		[Test]
		public void testNeqDateTime()
		{
			compareResourceEOE("equals/neqDateTime.e");
		}

		[Test]
		public void testNeqDecimal()
		{
			compareResourceEOE("equals/neqDecimal.e");
		}

		[Test]
		public void testNeqDict()
		{
			compareResourceEOE("equals/neqDict.e");
		}

		[Test]
		public void testNeqInteger()
		{
			compareResourceEOE("equals/neqInteger.e");
		}

		[Test]
		public void testNeqList()
		{
			compareResourceEOE("equals/neqList.e");
		}

		[Test]
		public void testNeqPeriod()
		{
			compareResourceEOE("equals/neqPeriod.e");
		}

		[Test]
		public void testNeqRange()
		{
			compareResourceEOE("equals/neqRange.e");
		}

		[Test]
		public void testNeqSet()
		{
			compareResourceEOE("equals/neqSet.e");
		}

		[Test]
		public void testNeqText()
		{
			compareResourceEOE("equals/neqText.e");
		}

		[Test]
		public void testNeqTime()
		{
			compareResourceEOE("equals/neqTime.e");
		}

		[Test]
		public void testReqText()
		{
			compareResourceEOE("equals/reqText.e");
		}

	}
}

