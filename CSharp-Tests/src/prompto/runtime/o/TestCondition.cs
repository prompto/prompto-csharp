using NUnit.Framework;
using prompto.parser;
using prompto.utils;

namespace prompto.runtime.o
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
		public void testEmbeddedIf()
		{
			CheckOutput("condition/embeddedIf.poc");
		}

		[Test]
		public void testLocalScope()
		{
			CheckOutput("condition/localScope.poc");
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

		[Test]
		public void testTernaryType()
		{
			CheckOutput("condition/ternaryType.poc");
		}

	}
}

