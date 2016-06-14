using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.ese
{

	[TestFixture]
	public class TestUuid : BaseEParserTest
	{

		[Test]
		public void testUuid()
		{
			compareResourceESE("uuid/uuid.pec");
		}

	}
}

