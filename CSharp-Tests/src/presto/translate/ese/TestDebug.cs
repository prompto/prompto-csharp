using NUnit.Framework;
using presto.parser;

namespace presto.translate.ese
{

	[TestFixture]
	public class TestDebug : BaseEParserTest
	{

		[Test]
		public void testStack()
		{
			compareResourceESE("debug/stack.pec");
		}

	}
}

