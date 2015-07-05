// generated: 2015-07-05T23:01:01.265
using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.ese
{

	[TestFixture]
	public class TestForward : BaseEParserTest
	{

		[Test]
		public void testForward()
		{
			compareResourceESE("forward/forward.pec");
		}

	}
}

