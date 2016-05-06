using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.ese
{

	[TestFixture]
	public class TestResourceError : BaseEParserTest
	{

		[Test]
		public void testBadRead()
		{
			compareResourceESE("resourceError/badRead.pec");
		}

		[Test]
		public void testBadResource()
		{
			compareResourceESE("resourceError/badResource.pec");
		}

		[Test]
		public void testBadWrite()
		{
			compareResourceESE("resourceError/badWrite.pec");
		}

	}
}

