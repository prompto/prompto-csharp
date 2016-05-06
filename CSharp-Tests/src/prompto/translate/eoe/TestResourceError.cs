using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.eoe
{

	[TestFixture]
	public class TestResourceError : BaseEParserTest
	{

		[Test]
		public void testBadRead()
		{
			compareResourceEOE("resourceError/badRead.pec");
		}

		[Test]
		public void testBadResource()
		{
			compareResourceEOE("resourceError/badResource.pec");
		}

		[Test]
		public void testBadWrite()
		{
			compareResourceEOE("resourceError/badWrite.pec");
		}

	}
}

