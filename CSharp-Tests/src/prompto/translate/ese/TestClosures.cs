using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.ese
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

		[Test]
		public void testParameterClosure()
		{
			compareResourceESE("closures/parameterClosure.pec");
		}

	}
}

