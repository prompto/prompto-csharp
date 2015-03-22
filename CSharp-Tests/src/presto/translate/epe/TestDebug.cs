using NUnit.Framework;
using presto.parser;

namespace presto.translate.epe
{

	[TestFixture]
	public class TestDebug : BaseEParserTest
	{

		[Test]
		public void testStack()
		{
			compareResourceEPE("debug/stack.e");
		}

	}
}

