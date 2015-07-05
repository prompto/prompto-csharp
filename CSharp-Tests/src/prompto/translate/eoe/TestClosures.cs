// generated: 2015-07-05T23:01:01.194
using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.eoe
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

