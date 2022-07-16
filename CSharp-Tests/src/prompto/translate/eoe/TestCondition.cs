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
		public void testEmbeddedIf()
		{
			compareResourceEOE("condition/embeddedIf.pec");
		}

		[Test]
		public void testReturnTextIf()
		{
			compareResourceEOE("condition/returnTextIf.pec");
		}

		[Test]
		public void testReturnVoidIf()
		{
			compareResourceEOE("condition/returnVoidIf.pec");
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

