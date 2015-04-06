using NUnit.Framework;
using presto.parser;

namespace presto.translate.ese
{

	[TestFixture]
	public class TestClosures : BaseEParserTest
	{

		[Test]
		public void testGlobalClosureNoArg()
		{
			compareResourceESE("closures/globalClosureNoArg.pec");
		}

		[Test]
		public void testGlobalClosureWithArg()
		{
			compareResourceESE("closures/globalClosureWithArg.pec");
		}

		[Test]
		public void testInstanceClosureNoArg()
		{
			compareResourceESE("closures/instanceClosureNoArg.pec");
		}

	}
}

