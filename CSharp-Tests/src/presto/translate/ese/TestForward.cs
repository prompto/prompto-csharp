using NUnit.Framework;
using presto.parser;

namespace presto.translate.ese
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

