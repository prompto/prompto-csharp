using NUnit.Framework;
using presto.parser;
using presto.utils;

namespace presto.runtime.o
{

	[TestFixture]
	public class TestCondition : BaseOParserTest
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
		public void testComplexIf()
		{
			CheckOutput("condition/complexIf.poc");
		}

		[Test]
		public void testElseIf()
		{
			CheckOutput("condition/elseIf.poc");
		}

		[Test]
		public void testReturnIf()
		{
			CheckOutput("condition/returnIf.poc");
		}

		[Test]
		public void testSimpleIf()
		{
			CheckOutput("condition/simpleIf.poc");
		}

		[Test]
		public void testSwitch()
		{
			CheckOutput("condition/switch.poc");
		}

		[Test]
		public void testTernary()
		{
			CheckOutput("condition/ternary.poc");
		}

	}
}

