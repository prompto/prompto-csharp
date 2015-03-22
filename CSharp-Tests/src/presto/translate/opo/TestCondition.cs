using NUnit.Framework;
using presto.parser;

namespace presto.translate.opo
{

	[TestFixture]
	public class TestCondition : BaseOParserTest
	{

		[Test]
		public void testComplexIf()
		{
			compareResourceOPO("condition/complexIf.o");
		}

		[Test]
		public void testElseIf()
		{
			compareResourceOPO("condition/elseIf.o");
		}

		[Test]
		public void testReturnIf()
		{
			compareResourceOPO("condition/returnIf.o");
		}

		[Test]
		public void testSimpleIf()
		{
			compareResourceOPO("condition/simpleIf.o");
		}

		[Test]
		public void testSwitch()
		{
			compareResourceOPO("condition/switch.o");
		}

		[Test]
		public void testTernary()
		{
			compareResourceOPO("condition/ternary.o");
		}

	}
}

