using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.omo
{

	[TestFixture]
	public class TestCondition : BaseOParserTest
	{

		[Test]
		public void testComplexIf()
		{
			compareResourceOMO("condition/complexIf.poc");
		}

		[Test]
		public void testEmbeddedIf()
		{
			compareResourceOMO("condition/embeddedIf.poc");
		}

		[Test]
		public void testLocalScope()
		{
			compareResourceOMO("condition/localScope.poc");
		}

		[Test]
		public void testReturnIf()
		{
			compareResourceOMO("condition/returnIf.poc");
		}

		[Test]
		public void testSimpleIf()
		{
			compareResourceOMO("condition/simpleIf.poc");
		}

		[Test]
		public void testSwitch()
		{
			compareResourceOMO("condition/switch.poc");
		}

		[Test]
		public void testTernary()
		{
			compareResourceOMO("condition/ternary.poc");
		}

		[Test]
		public void testTernaryType()
		{
			compareResourceOMO("condition/ternaryType.poc");
		}

	}
}

