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
			compareResourceEOE("resource/badRead.e");
		}

		[Test]
		public void testBadResource()
		{
			compareResourceEOE("resource/badResource.e");
		}

		[Test]
		public void testBadWrite()
		{
			compareResourceEOE("resource/badWrite.e");
		}

		[Test]
		public void testReadResource()
		{
			compareResourceEOE("resource/readResource.e");
		}

		[Test]
		public void testReadWithResource()
		{
			compareResourceEOE("resource/readWithResource.e");
		}

		[Test]
		public void testWriteResource()
		{
			compareResourceEOE("resource/writeResource.e");
		}

		[Test]
		public void testWriteWithResource()
		{
			compareResourceEOE("resource/writeWithResource.e");
		}

	}
}

