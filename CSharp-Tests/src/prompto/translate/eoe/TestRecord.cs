using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.eoe
{

	[TestFixture]
	public class TestRecord : BaseEParserTest
	{

		[Test]
		public void testRecord()
		{
			compareResourceEOE("record/record.pec");
		}

	}
}

