using NUnit.Framework;
using prompto.parser;
using prompto.utils;

namespace prompto.runtime.e
{

	[TestFixture]
	public class TestEquals : BaseEParserTest
	{

		[SetUp]
		public void before()
		{
			Out.init();
		}

		[TearDown]
		public void after()
		{
			Out.restore();
		}

		[Test]
		public void testEqBoolean()
		{
			CheckOutput("equals/eqBoolean.pec");
		}

		[Test]
		public void testEqCharacter()
		{
			CheckOutput("equals/eqCharacter.pec");
		}

		[Test]
		public void testEqDate()
		{
			CheckOutput("equals/eqDate.pec");
		}

		[Test]
		public void testEqDateTime()
		{
			CheckOutput("equals/eqDateTime.pec");
		}

		[Test]
		public void testEqDecimal()
		{
			CheckOutput("equals/eqDecimal.pec");
		}

		[Test]
		public void testEqDict()
		{
			CheckOutput("equals/eqDict.pec");
		}

		[Test]
		public void testEqInteger()
		{
			CheckOutput("equals/eqInteger.pec");
		}

		[Test]
		public void testEqList()
		{
			CheckOutput("equals/eqList.pec");
		}

		[Test]
		public void testEqPeriod()
		{
			CheckOutput("equals/eqPeriod.pec");
		}

		[Test]
		public void testEqRange()
		{
			CheckOutput("equals/eqRange.pec");
		}

		[Test]
		public void testEqSet()
		{
			CheckOutput("equals/eqSet.pec");
		}

		[Test]
		public void testEqText()
		{
			CheckOutput("equals/eqText.pec");
		}

		[Test]
		public void testEqTime()
		{
			CheckOutput("equals/eqTime.pec");
		}

		[Test]
		public void testEqVersion()
		{
			CheckOutput("equals/eqVersion.pec");
		}

		[Test]
		public void testIsBoolean()
		{
			CheckOutput("equals/isBoolean.pec");
		}

		[Test]
		public void testIsInstance()
		{
			CheckOutput("equals/isInstance.pec");
		}

		[Test]
		public void testIsNotBoolean()
		{
			CheckOutput("equals/isNotBoolean.pec");
		}

		[Test]
		public void testIsNotInstance()
		{
			CheckOutput("equals/isNotInstance.pec");
		}

		[Test]
		public void testNeqBoolean()
		{
			CheckOutput("equals/neqBoolean.pec");
		}

		[Test]
		public void testNeqCharacter()
		{
			CheckOutput("equals/neqCharacter.pec");
		}

		[Test]
		public void testNeqDate()
		{
			CheckOutput("equals/neqDate.pec");
		}

		[Test]
		public void testNeqDateTime()
		{
			CheckOutput("equals/neqDateTime.pec");
		}

		[Test]
		public void testNeqDecimal()
		{
			CheckOutput("equals/neqDecimal.pec");
		}

		[Test]
		public void testNeqDict()
		{
			CheckOutput("equals/neqDict.pec");
		}

		[Test]
		public void testNeqInteger()
		{
			CheckOutput("equals/neqInteger.pec");
		}

		[Test]
		public void testNeqList()
		{
			CheckOutput("equals/neqList.pec");
		}

		[Test]
		public void testNeqPeriod()
		{
			CheckOutput("equals/neqPeriod.pec");
		}

		[Test]
		public void testNeqRange()
		{
			CheckOutput("equals/neqRange.pec");
		}

		[Test]
		public void testNeqSet()
		{
			CheckOutput("equals/neqSet.pec");
		}

		[Test]
		public void testNeqText()
		{
			CheckOutput("equals/neqText.pec");
		}

		[Test]
		public void testNeqTime()
		{
			CheckOutput("equals/neqTime.pec");
		}

		[Test]
		public void testReqText()
		{
			CheckOutput("equals/reqText.pec");
		}

	}
}

