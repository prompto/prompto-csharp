using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.omo
{

	[TestFixture]
	public class TestAssign : BaseOParserTest
	{

		[Test]
		public void testDictEntry()
		{
			compareResourceOMO("assign/dictEntry.poc");
		}

	}
}

