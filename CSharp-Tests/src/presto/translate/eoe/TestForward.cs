using NUnit.Framework;
using presto.parser;

namespace presto.translate.eoe
{

	[TestFixture]
	public class TestForward : BaseEParserTest
	{

		[Test]
		public void testForward()
		{
			compareResourceEOE("forward/forward.e");
		}

	}
}

