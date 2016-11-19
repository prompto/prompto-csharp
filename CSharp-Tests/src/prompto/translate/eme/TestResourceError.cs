using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.eme
{

	[TestFixture]
	public class TestResourceError : BaseEParserTest
	{

		[Test]
		public void testBadRead()
		{
			compareResourceEME("resourceError/badRead.pec");
		}

		[Test]
		public void testBadResource()
		{
			compareResourceEME("resourceError/badResource.pec");
		}

		[Test]
		public void testBadWrite()
		{
			compareResourceEME("resourceError/badWrite.pec");
		}

	}
}

