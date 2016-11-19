using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.eme
{

	[TestFixture]
	public class TestEquals : BaseEParserTest
	{

		[Test]
		public void testEqBoolean()
		{
			compareResourceEME("equals/eqBoolean.pec");
		}

		[Test]
		public void testEqCharacter()
		{
			compareResourceEME("equals/eqCharacter.pec");
		}

		[Test]
		public void testEqDate()
		{
			compareResourceEME("equals/eqDate.pec");
		}

		[Test]
		public void testEqDateTime()
		{
			compareResourceEME("equals/eqDateTime.pec");
		}

		[Test]
		public void testEqDecimal()
		{
			compareResourceEME("equals/eqDecimal.pec");
		}

		[Test]
		public void testEqDict()
		{
			compareResourceEME("equals/eqDict.pec");
		}

		[Test]
		public void testEqInteger()
		{
			compareResourceEME("equals/eqInteger.pec");
		}

		[Test]
		public void testEqList()
		{
			compareResourceEME("equals/eqList.pec");
		}

		[Test]
		public void testEqPeriod()
		{
			compareResourceEME("equals/eqPeriod.pec");
		}

		[Test]
		public void testEqRange()
		{
			compareResourceEME("equals/eqRange.pec");
		}

		[Test]
		public void testEqSet()
		{
			compareResourceEME("equals/eqSet.pec");
		}

		[Test]
		public void testEqText()
		{
			compareResourceEME("equals/eqText.pec");
		}

		[Test]
		public void testEqTime()
		{
			compareResourceEME("equals/eqTime.pec");
		}

		[Test]
		public void testIsBoolean()
		{
			compareResourceEME("equals/isBoolean.pec");
		}

		[Test]
		public void testIsInstance()
		{
			compareResourceEME("equals/isInstance.pec");
		}

		[Test]
		public void testIsNotBoolean()
		{
			compareResourceEME("equals/isNotBoolean.pec");
		}

		[Test]
		public void testIsNotInstance()
		{
			compareResourceEME("equals/isNotInstance.pec");
		}

		[Test]
		public void testNeqBoolean()
		{
			compareResourceEME("equals/neqBoolean.pec");
		}

		[Test]
		public void testNeqCharacter()
		{
			compareResourceEME("equals/neqCharacter.pec");
		}

		[Test]
		public void testNeqDate()
		{
			compareResourceEME("equals/neqDate.pec");
		}

		[Test]
		public void testNeqDateTime()
		{
			compareResourceEME("equals/neqDateTime.pec");
		}

		[Test]
		public void testNeqDecimal()
		{
			compareResourceEME("equals/neqDecimal.pec");
		}

		[Test]
		public void testNeqDict()
		{
			compareResourceEME("equals/neqDict.pec");
		}

		[Test]
		public void testNeqInteger()
		{
			compareResourceEME("equals/neqInteger.pec");
		}

		[Test]
		public void testNeqList()
		{
			compareResourceEME("equals/neqList.pec");
		}

		[Test]
		public void testNeqPeriod()
		{
			compareResourceEME("equals/neqPeriod.pec");
		}

		[Test]
		public void testNeqRange()
		{
			compareResourceEME("equals/neqRange.pec");
		}

		[Test]
		public void testNeqSet()
		{
			compareResourceEME("equals/neqSet.pec");
		}

		[Test]
		public void testNeqText()
		{
			compareResourceEME("equals/neqText.pec");
		}

		[Test]
		public void testNeqTime()
		{
			compareResourceEME("equals/neqTime.pec");
		}

		[Test]
		public void testReqText()
		{
			compareResourceEME("equals/reqText.pec");
		}

	}
}

