using NUnit.Framework;
using presto.parser;

namespace presto.translate.oeo
{

	[TestFixture]
	public class TestCondition : BaseOParserTest
	{

		[Test]
		public void testComplexIf()
		{
			compareResourceOEO("condition/complexIf.o");
		}

		[Test]
		public void testElseIf()
		{
			compareResourceOEO("condition/elseIf.o");
		}

		[Test]
		public void testReturnIf()
		{
			compareResourceOEO("condition/returnIf.o");
		}

		[Test]
		public void testSimpleIf()
		{
			compareResourceOEO("condition/simpleIf.o");
		}

		[Test]
		public void testSwitch()
		{
			compareResourceOEO("condition/switch.o");
		}

		[Test]
		public void testTernary()
		{
			compareResourceOEO("condition/ternary.o");
		}

	}
}

