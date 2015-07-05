// generated: 2015-07-05T23:01:01.267
using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.oeo
{

	[TestFixture]
	public class TestForward : BaseOParserTest
	{

		[Test]
		public void testForward()
		{
			compareResourceOEO("forward/forward.poc");
		}

	}
}

