using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.oeo
{

	[TestFixture]
	public class TestUuid : BaseOParserTest
	{

		[Test]
		public void testUuid()
		{
			compareResourceOEO("uuid/uuid.poc");
		}

	}
}

