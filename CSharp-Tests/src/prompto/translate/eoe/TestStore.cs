using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.eoe
{

	[TestFixture]
	public class TestStore : BaseEParserTest
	{

		[Test]
		public void testRecord()
		{
			compareResourceEOE("store/record.pec");
		}

	}
}

