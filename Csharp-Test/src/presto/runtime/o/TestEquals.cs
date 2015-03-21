using NUnit.Framework;
using presto.parser;
using presto.utils;

namespace presto.runtime.o
{

	[TestFixture]
	public class TestEquals : BaseOParserTest
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
			CheckOutput("equals/eqBoolean.o");
		}

		[Test]
		public void testEqCharacter()
		{
			CheckOutput("equals/eqCharacter.o");
		}

		[Test]
		public void testEqDate()
		{
			CheckOutput("equals/eqDate.o");
		}

		[Test]
		public void testEqDateTime()
		{
			CheckOutput("equals/eqDateTime.o");
		}

		[Test]
		public void testEqDecimal()
		{
			CheckOutput("equals/eqDecimal.o");
		}

		[Test]
		public void testEqDict()
		{
			CheckOutput("equals/eqDict.o");
		}

		[Test]
		public void testEqInteger()
		{
			CheckOutput("equals/eqInteger.o");
		}

		[Test]
		public void testEqList()
		{
			CheckOutput("equals/eqList.o");
		}

		[Test]
		public void testEqPeriod()
		{
			CheckOutput("equals/eqPeriod.o");
		}

		[Test]
		public void testEqRange()
		{
			CheckOutput("equals/eqRange.o");
		}

		[Test]
		public void testEqSet()
		{
			CheckOutput("equals/eqSet.o");
		}

		[Test]
		public void testEqText()
		{
			CheckOutput("equals/eqText.o");
		}

		[Test]
		public void testEqTime()
		{
			CheckOutput("equals/eqTime.o");
		}

		[Test]
		public void testIsBoolean()
		{
			CheckOutput("equals/isBoolean.o");
		}

		[Test]
		public void testIsInstance()
		{
			CheckOutput("equals/isInstance.o");
		}

		[Test]
		public void testIsNotBoolean()
		{
			CheckOutput("equals/isNotBoolean.o");
		}

		[Test]
		public void testIsNotInstance()
		{
			CheckOutput("equals/isNotInstance.o");
		}

		[Test]
		public void testNeqBoolean()
		{
			CheckOutput("equals/neqBoolean.o");
		}

		[Test]
		public void testNeqCharacter()
		{
			CheckOutput("equals/neqCharacter.o");
		}

		[Test]
		public void testNeqDate()
		{
			CheckOutput("equals/neqDate.o");
		}

		[Test]
		public void testNeqDateTime()
		{
			CheckOutput("equals/neqDateTime.o");
		}

		[Test]
		public void testNeqDecimal()
		{
			CheckOutput("equals/neqDecimal.o");
		}

		[Test]
		public void testNeqDict()
		{
			CheckOutput("equals/neqDict.o");
		}

		[Test]
		public void testNeqInteger()
		{
			CheckOutput("equals/neqInteger.o");
		}

		[Test]
		public void testNeqList()
		{
			CheckOutput("equals/neqList.o");
		}

		[Test]
		public void testNeqPeriod()
		{
			CheckOutput("equals/neqPeriod.o");
		}

		[Test]
		public void testNeqRange()
		{
			CheckOutput("equals/neqRange.o");
		}

		[Test]
		public void testNeqSet()
		{
			CheckOutput("equals/neqSet.o");
		}

		[Test]
		public void testNeqText()
		{
			CheckOutput("equals/neqText.o");
		}

		[Test]
		public void testNeqTime()
		{
			CheckOutput("equals/neqTime.o");
		}

		[Test]
		public void testReqText()
		{
			CheckOutput("equals/reqText.o");
		}

	}
}

