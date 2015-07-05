// generated: 2015-07-05T23:01:01.197
using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.oeo
{

	[TestFixture]
	public class TestClosures : BaseOParserTest
	{

		[Test]
		public void testGlobalClosureNoArg()
		{
			compareResourceOEO("closures/globalClosureNoArg.poc");
		}

		[Test]
		public void testGlobalClosureWithArg()
		{
			compareResourceOEO("closures/globalClosureWithArg.poc");
		}

		[Test]
		public void testInstanceClosureNoArg()
		{
			compareResourceOEO("closures/instanceClosureNoArg.poc");
		}

	}
}

