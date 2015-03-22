using NUnit.Framework;
using presto.parser;

namespace presto.translate.epe
{

	[TestFixture]
	public class TestResource : BaseEParserTest
	{

		[Test]
		public void testBadRead()
		{
			compareResourceEPE("resource/badRead.e");
		}

		[Test]
		public void testBadResource()
		{
			compareResourceEPE("resource/badResource.e");
		}

		[Test]
		public void testBadWrite()
		{
			compareResourceEPE("resource/badWrite.e");
		}

		[Test]
		public void testReadResource()
		{
			compareResourceEPE("resource/readResource.e");
		}

		[Test]
		public void testReadWithResource()
		{
			compareResourceEPE("resource/readWithResource.e");
		}

		[Test]
		public void testWriteResource()
		{
			compareResourceEPE("resource/writeResource.e");
		}

		[Test]
		public void testWriteWithResource()
		{
			compareResourceEPE("resource/writeWithResource.e");
		}

	}
}

