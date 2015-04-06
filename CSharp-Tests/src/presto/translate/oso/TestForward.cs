using NUnit.Framework;
using presto.parser;

namespace presto.translate.oso
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

