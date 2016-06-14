using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.oso
{

	[TestFixture]
	public class TestUuid : BaseOParserTest
	{

		[Test]
		public void testUuid()
		{
			compareResourceOSO("uuid/uuid.poc");
		}

	}
}

