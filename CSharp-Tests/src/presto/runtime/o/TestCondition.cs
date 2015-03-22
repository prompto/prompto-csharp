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
			CheckOutput("condition/complexIf.o");
		}

		[Test]
		public void testElseIf()
		{
			CheckOutput("condition/elseIf.o");
		}

		[Test]
		public void testReturnIf()
		{
			CheckOutput("condition/returnIf.o");
		}

		[Test]
		public void testSimpleIf()
		{
			CheckOutput("condition/simpleIf.o");
		}

		[Test]
		public void testSwitch()
		{
			CheckOutput("condition/switch.o");
		}

		[Test]
		public void testTernary()
		{
			CheckOutput("condition/ternary.o");
		}

	}
}

