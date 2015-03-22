using NUnit.Framework;
using presto.parser;

namespace presto.translate.epe
{

	[TestFixture]
	public class TestCondition : BaseEParserTest
	{

		[Test]
		public void testComplexIf()
		{
			compareResourceEPE("condition/complexIf.e");
		}

		[Test]
		public void testElseIf()
		{
			compareResourceEPE("condition/elseIf.e");
		}

		[Test]
		public void testReturnIf()
		{
			compareResourceEPE("condition/returnIf.e");
		}

		[Test]
		public void testSimpleIf()
		{
			compareResourceEPE("condition/simpleIf.e");
		}

		[Test]
		public void testSwitch()
		{
			compareResourceEPE("condition/switch.e");
		}

		[Test]
		public void testTernary()
		{
			compareResourceEPE("condition/ternary.e");
		}

	}
}

