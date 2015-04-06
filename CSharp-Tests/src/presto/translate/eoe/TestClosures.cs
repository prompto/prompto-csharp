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
			compareResourceEOE("closures/globalClosureNoArg.pec");
		}

		[Test]
		public void testGlobalClosureWithArg()
		{
			compareResourceEOE("closures/globalClosureWithArg.pec");
		}

		[Test]
		public void testInstanceClosureNoArg()
		{
			compareResourceEOE("closures/instanceClosureNoArg.pec");
		}

	}
}

