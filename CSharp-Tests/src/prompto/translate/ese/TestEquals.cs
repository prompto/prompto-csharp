using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.ese
{

	[TestFixture]
	public class TestEquals : BaseEParserTest
	{

		[Test]
		public void testEqBoolean()
		{
			compareResourceESE("equals/eqBoolean.pec");
		}

		[Test]
		public void testEqCharacter()
		{
			compareResourceESE("equals/eqCharacter.pec");
		}

		[Test]
		public void testEqDate()
		{
			compareResourceESE("equals/eqDate.pec");
		}

		[Test]
		public void testEqDateTime()
		{
			compareResourceESE("equals/eqDateTime.pec");
		}

		[Test]
		public void testEqDecimal()
		{
			compareResourceESE("equals/eqDecimal.pec");
		}

		[Test]
		public void testEqDict()
		{
			compareResourceESE("equals/eqDict.pec");
		}

		[Test]
		public void testEqInteger()
		{
			compareResourceESE("equals/eqInteger.pec");
		}

		[Test]
		public void testEqList()
		{
			compareResourceESE("equals/eqList.pec");
		}

		[Test]
		public void testEqPeriod()
		{
			compareResourceESE("equals/eqPeriod.pec");
		}

		[Test]
		public void testEqRange()
		{
			compareResourceESE("equals/eqRange.pec");
		}

		[Test]
		public void testEqSet()
		{
			compareResourceESE("equals/eqSet.pec");
		}

		[Test]
		public void testEqText()
		{
			compareResourceESE("equals/eqText.pec");
		}

		[Test]
		public void testEqTime()
		{
			compareResourceESE("equals/eqTime.pec");
		}

		[Test]
		public void testIsBoolean()
		{
			compareResourceESE("equals/isBoolean.pec");
		}

		[Test]
		public void testIsInstance()
		{
			compareResourceESE("equals/isInstance.pec");
		}

		[Test]
		public void testIsNotBoolean()
		{
			compareResourceESE("equals/isNotBoolean.pec");
		}

		[Test]
		public void testIsNotInstance()
		{
			compareResourceESE("equals/isNotInstance.pec");
		}

		[Test]
		public void testNeqBoolean()
		{
			compareResourceESE("equals/neqBoolean.pec");
		}

		[Test]
		public void testNeqCharacter()
		{
			compareResourceESE("equals/neqCharacter.pec");
		}

		[Test]
		public void testNeqDate()
		{
			compareResourceESE("equals/neqDate.pec");
		}

		[Test]
		public void testNeqDateTime()
		{
			compareResourceESE("equals/neqDateTime.pec");
		}

		[Test]
		public void testNeqDecimal()
		{
			compareResourceESE("equals/neqDecimal.pec");
		}

		[Test]
		public void testNeqDict()
		{
			compareResourceESE("equals/neqDict.pec");
		}

		[Test]
		public void testNeqInteger()
		{
			compareResourceESE("equals/neqInteger.pec");
		}

		[Test]
		public void testNeqList()
		{
			compareResourceESE("equals/neqList.pec");
		}

		[Test]
		public void testNeqPeriod()
		{
			compareResourceESE("equals/neqPeriod.pec");
		}

		[Test]
		public void testNeqRange()
		{
			compareResourceESE("equals/neqRange.pec");
		}

		[Test]
		public void testNeqSet()
		{
			compareResourceESE("equals/neqSet.pec");
		}

		[Test]
		public void testNeqText()
		{
			compareResourceESE("equals/neqText.pec");
		}

		[Test]
		public void testNeqTime()
		{
			compareResourceESE("equals/neqTime.pec");
		}

		[Test]
		public void testReqText()
		{
			compareResourceESE("equals/reqText.pec");
		}

	}
}

