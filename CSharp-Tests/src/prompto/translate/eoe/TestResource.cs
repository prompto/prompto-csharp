using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.eoe
{

	[TestFixture]
	public class TestResource : BaseEParserTest
	{

		[Test]
		public void testReadResource()
		{
			compareResourceEOE("resource/readResource.pec");
		}

		[Test]
		public void testReadWithResource()
		{
			compareResourceEOE("resource/readWithResource.pec");
		}

		[Test]
		public void testWriteResource()
		{
			compareResourceEOE("resource/writeResource.pec");
		}

		[Test]
		public void testWriteWithResource()
		{
			compareResourceEOE("resource/writeWithResource.pec");
		}

	}
}

