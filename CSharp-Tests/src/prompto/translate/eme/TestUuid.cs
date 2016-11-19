using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.eme
{

	[TestFixture]
	public class TestUuid : BaseEParserTest
	{

		[Test]
		public void testUuid()
		{
			compareResourceEME("uuid/uuid.pec");
		}

	}
}

