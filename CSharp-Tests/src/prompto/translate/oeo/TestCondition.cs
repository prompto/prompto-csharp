using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.oeo
{

	[TestFixture]
	public class TestCondition : BaseOParserTest
	{

		[Test]
		public void testComplexIf()
		{
			compareResourceOEO("condition/complexIf.poc");
		}

		[Test]
		public void testEmbeddedIf()
		{
			compareResourceOEO("condition/embeddedIf.poc");
		}

		[Test]
		public void testReturnIf()
		{
			compareResourceOEO("condition/returnIf.poc");
		}

		[Test]
		public void testSimpleIf()
		{
			compareResourceOEO("condition/simpleIf.poc");
		}

		[Test]
		public void testSwitch()
		{
			compareResourceOEO("condition/switch.poc");
		}

		[Test]
		public void testTernary()
		{
			compareResourceOEO("condition/ternary.poc");
		}

		[Test]
		public void testTernaryType()
		{
			compareResourceOEO("condition/ternaryType.poc");
		}

	}
}

