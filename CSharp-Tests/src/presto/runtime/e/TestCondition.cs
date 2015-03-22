using NUnit.Framework;
using presto.parser;
using presto.utils;

namespace presto.runtime.e
{

	[TestFixture]
	public class TestCondition : BaseEParserTest
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
			CheckOutput("condition/complexIf.e");
		}

		[Test]
		public void testElseIf()
		{
			CheckOutput("condition/elseIf.e");
		}

		[Test]
		public void testReturnIf()
		{
			CheckOutput("condition/returnIf.e");
		}

		[Test]
		public void testSimpleIf()
		{
			CheckOutput("condition/simpleIf.e");
		}

		[Test]
		public void testSwitch()
		{
			CheckOutput("condition/switch.e");
		}

		[Test]
		public void testTernary()
		{
			CheckOutput("condition/ternary.e");
		}

	}
}

