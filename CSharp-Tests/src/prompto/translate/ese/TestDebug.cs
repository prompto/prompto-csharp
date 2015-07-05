// generated: 2015-07-05T23:01:01.218
using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.ese
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

