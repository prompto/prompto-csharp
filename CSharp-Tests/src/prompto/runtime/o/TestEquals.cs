// generated: 2015-07-05T23:01:01.248
using NUnit.Framework;
using prompto.parser;
using prompto.utils;

namespace prompto.runtime.o
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
			CheckOutput("equals/eqBoolean.poc");
		}

		[Test]
		public void testEqCharacter()
		{
			CheckOutput("equals/eqCharacter.poc");
		}

		[Test]
		public void testEqDate()
		{
			CheckOutput("equals/eqDate.poc");
		}

		[Test]
		public void testEqDateTime()
		{
			CheckOutput("equals/eqDateTime.poc");
		}

		[Test]
		public void testEqDecimal()
		{
			CheckOutput("equals/eqDecimal.poc");
		}

		[Test]
		public void testEqDict()
		{
			CheckOutput("equals/eqDict.poc");
		}

		[Test]
		public void testEqInteger()
		{
			CheckOutput("equals/eqInteger.poc");
		}

		[Test]
		public void testEqList()
		{
			CheckOutput("equals/eqList.poc");
		}

		[Test]
		public void testEqPeriod()
		{
			CheckOutput("equals/eqPeriod.poc");
		}

		[Test]
		public void testEqRange()
		{
			CheckOutput("equals/eqRange.poc");
		}

		[Test]
		public void testEqSet()
		{
			CheckOutput("equals/eqSet.poc");
		}

		[Test]
		public void testEqText()
		{
			CheckOutput("equals/eqText.poc");
		}

		[Test]
		public void testEqTime()
		{
			CheckOutput("equals/eqTime.poc");
		}

		[Test]
		public void testIsBoolean()
		{
			CheckOutput("equals/isBoolean.poc");
		}

		[Test]
		public void testIsInstance()
		{
			CheckOutput("equals/isInstance.poc");
		}

		[Test]
		public void testIsNotBoolean()
		{
			CheckOutput("equals/isNotBoolean.poc");
		}

		[Test]
		public void testIsNotInstance()
		{
			CheckOutput("equals/isNotInstance.poc");
		}

		[Test]
		public void testNeqBoolean()
		{
			CheckOutput("equals/neqBoolean.poc");
		}

		[Test]
		public void testNeqCharacter()
		{
			CheckOutput("equals/neqCharacter.poc");
		}

		[Test]
		public void testNeqDate()
		{
			CheckOutput("equals/neqDate.poc");
		}

		[Test]
		public void testNeqDateTime()
		{
			CheckOutput("equals/neqDateTime.poc");
		}

		[Test]
		public void testNeqDecimal()
		{
			CheckOutput("equals/neqDecimal.poc");
		}

		[Test]
		public void testNeqDict()
		{
			CheckOutput("equals/neqDict.poc");
		}

		[Test]
		public void testNeqInteger()
		{
			CheckOutput("equals/neqInteger.poc");
		}

		[Test]
		public void testNeqList()
		{
			CheckOutput("equals/neqList.poc");
		}

		[Test]
		public void testNeqPeriod()
		{
			CheckOutput("equals/neqPeriod.poc");
		}

		[Test]
		public void testNeqRange()
		{
			CheckOutput("equals/neqRange.poc");
		}

		[Test]
		public void testNeqSet()
		{
			CheckOutput("equals/neqSet.poc");
		}

		[Test]
		public void testNeqText()
		{
			CheckOutput("equals/neqText.poc");
		}

		[Test]
		public void testNeqTime()
		{
			CheckOutput("equals/neqTime.poc");
		}

		[Test]
		public void testReqText()
		{
			CheckOutput("equals/reqText.poc");
		}

	}
}

