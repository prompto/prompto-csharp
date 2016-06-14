using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.eoe
{

	[TestFixture]
	public class TestUuid : BaseEParserTest
	{

		[Test]
		public void testUuid()
		{
			compareResourceEOE("uuid/uuid.pec");
		}

	}
}

