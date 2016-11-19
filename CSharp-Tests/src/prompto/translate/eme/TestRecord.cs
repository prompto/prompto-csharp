using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.ese
{

	[TestFixture]
	public class TestRecord : BaseEParserTest
	{

		[Test]
		public void testRecord()
		{
			compareResourceESE("record/record.pec");
		}

	}
}

