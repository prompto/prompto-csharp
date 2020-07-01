using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.oeo
{

	[TestFixture]
	public class TestAssign : BaseOParserTest
	{

		[Test]
		public void testDictEntry()
		{
			compareResourceOEO("assign/dictEntry.poc");
		}

	}
}

