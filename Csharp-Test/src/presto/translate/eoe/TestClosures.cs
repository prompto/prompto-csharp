using NUnit.Framework;
using presto.parser;

namespace presto.translate.eoe
{

	[TestFixture]
	public class TestClosures : BaseEParserTest
	{

		[Test]
		public void testGlobalClosureNoArg()
		{
			compareResourceEOE("closures/globalClosureNoArg.e");
		}

		[Test]
		public void testGlobalClosureWithArg()
		{
			compareResourceEOE("closures/globalClosureWithArg.e");
		}

		[Test]
		public void testInstanceClosureNoArg()
		{
			compareResourceEOE("closures/instanceClosureNoArg.e");
		}

	}
}

