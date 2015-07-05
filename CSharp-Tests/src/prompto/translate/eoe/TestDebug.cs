// generated: 2015-07-05T23:01:01.217
using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.eoe
{

	[TestFixture]
	public class TestDebug : BaseEParserTest
	{

		[Test]
		public void testStack()
		{
			compareResourceEOE("debug/stack.pec");
		}

	}
}

