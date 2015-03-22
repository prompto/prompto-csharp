using NUnit.Framework;
using presto.parser;

namespace presto.translate.epe
{

	[TestFixture]
	public class TestEquals : BaseEParserTest
	{

		[Test]
		public void testEqBoolean()
		{
			compareResourceEPE("equals/eqBoolean.e");
		}

		[Test]
		public void testEqCharacter()
		{
			compareResourceEPE("equals/eqCharacter.e");
		}

		[Test]
		public void testEqDate()
		{
			compareResourceEPE("equals/eqDate.e");
		}

		[Test]
		public void testEqDateTime()
		{
			compareResourceEPE("equals/eqDateTime.e");
		}

		[Test]
		public void testEqDecimal()
		{
			compareResourceEPE("equals/eqDecimal.e");
		}

		[Test]
		public void testEqDict()
		{
			compareResourceEPE("equals/eqDict.e");
		}

		[Test]
		public void testEqInteger()
		{
			compareResourceEPE("equals/eqInteger.e");
		}

		[Test]
		public void testEqList()
		{
			compareResourceEPE("equals/eqList.e");
		}

		[Test]
		public void testEqPeriod()
		{
			compareResourceEPE("equals/eqPeriod.e");
		}

		[Test]
		public void testEqRange()
		{
			compareResourceEPE("equals/eqRange.e");
		}

		[Test]
		public void testEqSet()
		{
			compareResourceEPE("equals/eqSet.e");
		}

		[Test]
		public void testEqText()
		{
			compareResourceEPE("equals/eqText.e");
		}

		[Test]
		public void testEqTime()
		{
			compareResourceEPE("equals/eqTime.e");
		}

		[Test]
		public void testIsBoolean()
		{
			compareResourceEPE("equals/isBoolean.e");
		}

		[Test]
		public void testIsInstance()
		{
			compareResourceEPE("equals/isInstance.e");
		}

		[Test]
		public void testIsNotBoolean()
		{
			compareResourceEPE("equals/isNotBoolean.e");
		}

		[Test]
		public void testIsNotInstance()
		{
			compareResourceEPE("equals/isNotInstance.e");
		}

		[Test]
		public void testNeqBoolean()
		{
			compareResourceEPE("equals/neqBoolean.e");
		}

		[Test]
		public void testNeqCharacter()
		{
			compareResourceEPE("equals/neqCharacter.e");
		}

		[Test]
		public void testNeqDate()
		{
			compareResourceEPE("equals/neqDate.e");
		}

		[Test]
		public void testNeqDateTime()
		{
			compareResourceEPE("equals/neqDateTime.e");
		}

		[Test]
		public void testNeqDecimal()
		{
			compareResourceEPE("equals/neqDecimal.e");
		}

		[Test]
		public void testNeqDict()
		{
			compareResourceEPE("equals/neqDict.e");
		}

		[Test]
		public void testNeqInteger()
		{
			compareResourceEPE("equals/neqInteger.e");
		}

		[Test]
		public void testNeqList()
		{
			compareResourceEPE("equals/neqList.e");
		}

		[Test]
		public void testNeqPeriod()
		{
			compareResourceEPE("equals/neqPeriod.e");
		}

		[Test]
		public void testNeqRange()
		{
			compareResourceEPE("equals/neqRange.e");
		}

		[Test]
		public void testNeqSet()
		{
			compareResourceEPE("equals/neqSet.e");
		}

		[Test]
		public void testNeqText()
		{
			compareResourceEPE("equals/neqText.e");
		}

		[Test]
		public void testNeqTime()
		{
			compareResourceEPE("equals/neqTime.e");
		}

		[Test]
		public void testReqText()
		{
			compareResourceEPE("equals/reqText.e");
		}

	}
}

