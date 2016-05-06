using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.oso
{

	[TestFixture]
	public class TestResourceError : BaseOParserTest
	{

		[Test]
		public void testBadRead()
		{
			compareResourceOSO("resourceError/badRead.poc");
		}

		[Test]
		public void testBadResource()
		{
			compareResourceOSO("resourceError/badResource.poc");
		}

		[Test]
		public void testBadWrite()
		{
			compareResourceOSO("resourceError/badWrite.poc");
		}

	}
}

