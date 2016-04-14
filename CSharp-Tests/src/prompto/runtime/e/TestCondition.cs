using NUnit.Framework;
using prompto.parser;
using prompto.utils;

namespace prompto.runtime.e
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
			CheckOutput("condition/complexIf.pec");
		}

		[Test]
		public void testReturnIf()
		{
			CheckOutput("condition/returnIf.pec");
		}

		[Test]
		public void testSimpleIf()
		{
			CheckOutput("condition/simpleIf.pec");
		}

		[Test]
		public void testSwitch()
		{
			CheckOutput("condition/switch.pec");
		}

		[Test]
		public void testTernary()
		{
			CheckOutput("condition/ternary.pec");
		}

	}
}

