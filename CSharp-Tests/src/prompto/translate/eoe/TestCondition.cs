// generated: 2015-07-05T23:01:01.201
using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.eoe
{

	[TestFixture]
	public class TestCondition : BaseEParserTest
	{

		[Test]
		public void testComplexIf()
		{
			compareResourceEOE("condition/complexIf.pec");
		}

		[Test]
		public void testElseIf()
		{
			compareResourceEOE("condition/elseIf.pec");
		}

		[Test]
		public void testReturnIf()
		{
			compareResourceEOE("condition/returnIf.pec");
		}

		[Test]
		public void testSimpleIf()
		{
			compareResourceEOE("condition/simpleIf.pec");
		}

		[Test]
		public void testSwitch()
		{
			compareResourceEOE("condition/switch.pec");
		}

		[Test]
		public void testTernary()
		{
			compareResourceEOE("condition/ternary.pec");
		}

	}
}

