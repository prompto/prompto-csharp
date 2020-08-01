using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.eoe
{

	[TestFixture]
	public class TestManual : BaseEParserTest
	{

		[Test]
		public void testScheduler()
		{
			compareResourceEOE("manual/scheduler.pec");
		}

	}
}

