using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.eme
{

	[TestFixture]
	public class TestCondition : BaseEParserTest
	{

		[Test]
		public void testComplexIf()
		{
			compareResourceEME("condition/complexIf.pec");
		}

		[Test]
		public void testEmbeddedIf()
		{
			compareResourceEME("condition/embeddedIf.pec");
		}

		[Test]
		public void testReturnTextIf()
		{
			compareResourceEME("condition/returnTextIf.pec");
		}

		[Test]
		public void testReturnVoidIf()
		{
			compareResourceEME("condition/returnVoidIf.pec");
		}

		[Test]
		public void testSimpleIf()
		{
			compareResourceEME("condition/simpleIf.pec");
		}

		[Test]
		public void testSwitch()
		{
			compareResourceEME("condition/switch.pec");
		}

		[Test]
		public void testTernary()
		{
			compareResourceEME("condition/ternary.pec");
		}

	}
}

