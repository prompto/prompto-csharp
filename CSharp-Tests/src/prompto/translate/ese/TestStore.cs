using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.ese
{

	[TestFixture]
	public class TestStore : BaseEParserTest
	{

		[Test]
		public void testRecord()
		{
			compareResourceESE("store/record.pec");
		}

	}
}

