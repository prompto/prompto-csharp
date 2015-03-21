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
			compareResourceOEO("closures/globalClosureNoArg.o");
		}

		[Test]
		public void testGlobalClosureWithArg()
		{
			compareResourceOEO("closures/globalClosureWithArg.o");
		}

		[Test]
		public void testInstanceClosureNoArg()
		{
			compareResourceOEO("closures/instanceClosureNoArg.o");
		}

	}
}

