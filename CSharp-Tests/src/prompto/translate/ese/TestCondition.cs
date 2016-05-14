using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.ese
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
		public void testEmbeddedIf()
		{
			compareResourceESE("condition/embeddedIf.pec");
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

