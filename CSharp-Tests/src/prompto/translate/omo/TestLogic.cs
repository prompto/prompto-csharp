using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.omo
{

	[TestFixture]
	public class TestLogic : BaseOParserTest
	{

		[Test]
		public void testAndBoolean()
		{
			compareResourceOMO("logic/andBoolean.poc");
		}

		[Test]
		public void testNotBoolean()
		{
			compareResourceOMO("logic/notBoolean.poc");
		}

		[Test]
		public void testOrBoolean()
		{
			compareResourceOMO("logic/orBoolean.poc");
		}

		[Test]
		public void testRightSkipped()
		{
			compareResourceOMO("logic/rightSkipped.poc");
		}

	}
}

