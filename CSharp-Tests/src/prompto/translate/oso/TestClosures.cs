using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.oso
{

	[TestFixture]
	public class TestClosures : BaseOParserTest
	{

		[Test]
		public void testGlobalClosureNoArg()
		{
			compareResourceOSO("closures/globalClosureNoArg.poc");
		}

		[Test]
		public void testGlobalClosureWithArg()
		{
			compareResourceOSO("closures/globalClosureWithArg.poc");
		}

		[Test]
		public void testInstanceClosureNoArg()
		{
			compareResourceOSO("closures/instanceClosureNoArg.poc");
		}

	}
}

