using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.eme
{

	[TestFixture]
	public class TestManual : BaseEParserTest
	{

		[Test]
		public void testScheduler()
		{
			compareResourceEME("manual/scheduler.pec");
		}

	}
}

