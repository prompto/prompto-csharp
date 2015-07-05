// generated: 2015-07-05T23:01:01.264
using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.eoe
{

	[TestFixture]
	public class TestForward : BaseEParserTest
	{

		[Test]
		public void testForward()
		{
			compareResourceEOE("forward/forward.pec");
		}

	}
}

