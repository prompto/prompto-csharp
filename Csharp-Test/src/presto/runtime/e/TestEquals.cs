using NUnit.Framework;
using presto.parser;
using presto.utils;

namespace presto.runtime.e
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
			CheckOutput("equals/eqBoolean.e");
		}

		[Test]
		public void testEqCharacter()
		{
			CheckOutput("equals/eqCharacter.e");
		}

		[Test]
		public void testEqDate()
		{
			CheckOutput("equals/eqDate.e");
		}

		[Test]
		public void testEqDateTime()
		{
			CheckOutput("equals/eqDateTime.e");
		}

		[Test]
		public void testEqDecimal()
		{
			CheckOutput("equals/eqDecimal.e");
		}

		[Test]
		public void testEqDict()
		{
			CheckOutput("equals/eqDict.e");
		}

		[Test]
		public void testEqInteger()
		{
			CheckOutput("equals/eqInteger.e");
		}

		[Test]
		public void testEqList()
		{
			CheckOutput("equals/eqList.e");
		}

		[Test]
		public void testEqPeriod()
		{
			CheckOutput("equals/eqPeriod.e");
		}

		[Test]
		public void testEqRange()
		{
			CheckOutput("equals/eqRange.e");
		}

		[Test]
		public void testEqSet()
		{
			CheckOutput("equals/eqSet.e");
		}

		[Test]
		public void testEqText()
		{
			CheckOutput("equals/eqText.e");
		}

		[Test]
		public void testEqTime()
		{
			CheckOutput("equals/eqTime.e");
		}

		[Test]
		public void testIsBoolean()
		{
			CheckOutput("equals/isBoolean.e");
		}

		[Test]
		public void testIsInstance()
		{
			CheckOutput("equals/isInstance.e");
		}

		[Test]
		public void testIsNotBoolean()
		{
			CheckOutput("equals/isNotBoolean.e");
		}

		[Test]
		public void testIsNotInstance()
		{
			CheckOutput("equals/isNotInstance.e");
		}

		[Test]
		public void testNeqBoolean()
		{
			CheckOutput("equals/neqBoolean.e");
		}

		[Test]
		public void testNeqCharacter()
		{
			CheckOutput("equals/neqCharacter.e");
		}

		[Test]
		public void testNeqDate()
		{
			CheckOutput("equals/neqDate.e");
		}

		[Test]
		public void testNeqDateTime()
		{
			CheckOutput("equals/neqDateTime.e");
		}

		[Test]
		public void testNeqDecimal()
		{
			CheckOutput("equals/neqDecimal.e");
		}

		[Test]
		public void testNeqDict()
		{
			CheckOutput("equals/neqDict.e");
		}

		[Test]
		public void testNeqInteger()
		{
			CheckOutput("equals/neqInteger.e");
		}

		[Test]
		public void testNeqList()
		{
			CheckOutput("equals/neqList.e");
		}

		[Test]
		public void testNeqPeriod()
		{
			CheckOutput("equals/neqPeriod.e");
		}

		[Test]
		public void testNeqRange()
		{
			CheckOutput("equals/neqRange.e");
		}

		[Test]
		public void testNeqSet()
		{
			CheckOutput("equals/neqSet.e");
		}

		[Test]
		public void testNeqText()
		{
			CheckOutput("equals/neqText.e");
		}

		[Test]
		public void testNeqTime()
		{
			CheckOutput("equals/neqTime.e");
		}

		[Test]
		public void testReqText()
		{
			CheckOutput("equals/reqText.e");
		}

	}
}

