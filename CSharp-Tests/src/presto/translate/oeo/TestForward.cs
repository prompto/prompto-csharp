using NUnit.Framework;
using presto.parser;

namespace presto.translate.oeo
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

