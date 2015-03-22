using NUnit.Framework;
using presto.parser;

namespace presto.translate.epe
{

	[TestFixture]
	public class TestClosures : BaseEParserTest
	{

		[Test]
		public void testGlobalClosureNoArg()
		{
			compareResourceEPE("closures/globalClosureNoArg.e");
		}

		[Test]
		public void testGlobalClosureWithArg()
		{
			compareResourceEPE("closures/globalClosureWithArg.e");
		}

		[Test]
		public void testInstanceClosureNoArg()
		{
			compareResourceEPE("closures/instanceClosureNoArg.e");
		}

	}
}

