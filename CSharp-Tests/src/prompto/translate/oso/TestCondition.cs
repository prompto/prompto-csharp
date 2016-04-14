using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.oso
{

	[TestFixture]
	public class TestCondition : BaseOParserTest
	{

		[Test]
		public void testComplexIf()
		{
			compareResourceOSO("condition/complexIf.poc");
		}

		[Test]
		public void testReturnIf()
		{
			compareResourceOSO("condition/returnIf.poc");
		}

		[Test]
		public void testSimpleIf()
		{
			compareResourceOSO("condition/simpleIf.poc");
		}

		[Test]
		public void testSwitch()
		{
			compareResourceOSO("condition/switch.poc");
		}

		[Test]
		public void testTernary()
		{
			compareResourceOSO("condition/ternary.poc");
		}

	}
}

