// generated: 2015-07-05T23:01:01.268
using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.oso
{

	[TestFixture]
	public class TestForward : BaseOParserTest
	{

		[Test]
		public void testForward()
		{
			compareResourceOSO("forward/forward.poc");
		}

	}
}

