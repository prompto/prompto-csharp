using NUnit.Framework;
using presto.parser;

namespace presto.translate.opo
{

	[TestFixture]
	public class TestForward : BaseOParserTest
	{

		[Test]
		public void testForward()
		{
			compareResourceOPO("forward/forward.o");
		}

	}
}

