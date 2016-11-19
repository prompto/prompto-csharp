using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.omo
{

	[TestFixture]
	public class TestUuid : BaseOParserTest
	{

		[Test]
		public void testUuid()
		{
			compareResourceOMO("uuid/uuid.poc");
		}

	}
}

