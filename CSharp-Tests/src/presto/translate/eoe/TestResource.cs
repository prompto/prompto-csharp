using NUnit.Framework;
using presto.parser;

namespace presto.translate.eoe
{

	[TestFixture]
	public class TestResource : BaseEParserTest
	{

		[Test]
		public void testBadRead()
		{
			compareResourceEOE("resource/badRead.pec");
		}

		[Test]
		public void testBadResource()
		{
			compareResourceEOE("resource/badResource.pec");
		}

		[Test]
		public void testBadWrite()
		{
			compareResourceEOE("resource/badWrite.pec");
		}

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

