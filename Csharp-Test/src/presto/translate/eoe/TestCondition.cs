using NUnit.Framework;
using presto.parser;

namespace presto.translate.eoe
{

	[TestFixture]
	public class TestCondition : BaseEParserTest
	{

		[Test]
		public void testComplexIf()
		{
			compareResourceEOE("condition/complexIf.e");
		}

		[Test]
		public void testElseIf()
		{
			compareResourceEOE("condition/elseIf.e");
		}

		[Test]
		public void testReturnIf()
		{
			compareResourceEOE("condition/returnIf.e");
		}

		[Test]
		public void testSimpleIf()
		{
			compareResourceEOE("condition/simpleIf.e");
		}

		[Test]
		public void testSwitch()
		{
			compareResourceEOE("condition/switch.e");
		}

		[Test]
		public void testTernary()
		{
			compareResourceEOE("condition/ternary.e");
		}

	}
}

