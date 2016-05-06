using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.oeo
{

	[TestFixture]
	public class TestResourceError : BaseOParserTest
	{

		[Test]
		public void testBadRead()
		{
			compareResourceOEO("resourceError/badRead.poc");
		}

		[Test]
		public void testBadResource()
		{
			compareResourceOEO("resourceError/badResource.poc");
		}

		[Test]
		public void testBadWrite()
		{
			compareResourceOEO("resourceError/badWrite.poc");
		}

	}
}

