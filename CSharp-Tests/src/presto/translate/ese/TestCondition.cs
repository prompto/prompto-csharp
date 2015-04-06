using NUnit.Framework;
using presto.parser;

namespace presto.translate.ese
{

	[TestFixture]
	public class TestCondition : BaseEParserTest
	{

		[Test]
		public void testComplexIf()
		{
			compareResourceESE("condition/complexIf.pec");
		}

		[Test]
		public void testElseIf()
		{
			compareResourceESE("condition/elseIf.pec");
		}

		[Test]
		public void testReturnIf()
		{
			compareResourceESE("condition/returnIf.pec");
		}

		[Test]
		public void testSimpleIf()
		{
			compareResourceESE("condition/simpleIf.pec");
		}

		[Test]
		public void testSwitch()
		{
			compareResourceESE("condition/switch.pec");
		}

		[Test]
		public void testTernary()
		{
			compareResourceESE("condition/ternary.pec");
		}

	}
}

