using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.omo
{

	[TestFixture]
	public class TestResourceError : BaseOParserTest
	{

		[Test]
		public void testBadRead()
		{
			compareResourceOMO("resourceError/badRead.poc");
		}

		[Test]
		public void testBadResource()
		{
			compareResourceOMO("resourceError/badResource.poc");
		}

		[Test]
		public void testBadWrite()
		{
			compareResourceOMO("resourceError/badWrite.poc");
		}

	}
}

