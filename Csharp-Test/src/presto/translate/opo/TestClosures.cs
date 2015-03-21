using NUnit.Framework;
using presto.parser;

namespace presto.translate.opo
{

	[TestFixture]
	public class TestClosures : BaseOParserTest
	{

		[Test]
		public void testGlobalClosureNoArg()
		{
			compareResourceOPO("closures/globalClosureNoArg.o");
		}

		[Test]
		public void testGlobalClosureWithArg()
		{
			compareResourceOPO("closures/globalClosureWithArg.o");
		}

		[Test]
		public void testInstanceClosureNoArg()
		{
			compareResourceOPO("closures/instanceClosureNoArg.o");
		}

	}
}

