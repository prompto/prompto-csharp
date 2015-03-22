using NUnit.Framework;
using presto.parser;

namespace presto.translate.epe
{

	[TestFixture]
	public class TestForward : BaseEParserTest
	{

		[Test]
		public void testForward()
		{
			compareResourceEPE("forward/forward.e");
		}

	}
}

