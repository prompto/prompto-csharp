using NUnit.Framework;
using presto.parser;

namespace presto.translate.oeo
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

