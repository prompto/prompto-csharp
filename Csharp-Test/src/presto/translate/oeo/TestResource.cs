using NUnit.Framework;
using presto.parser;

namespace presto.translate.oeo
{

	[TestFixture]
	public class TestResource : BaseOParserTest
	{

		[Test]
		public void testBadRead()
		{
			compareResourceOEO("resource/badRead.o");
		}

		[Test]
		public void testBadResource()
		{
			compareResourceOEO("resource/badResource.o");
		}

		[Test]
		public void testBadWrite()
		{
			compareResourceOEO("resource/badWrite.o");
		}

		[Test]
		public void testReadResource()
		{
			compareResourceOEO("resource/readResource.o");
		}

		[Test]
		public void testReadWithResource()
		{
			compareResourceOEO("resource/readWithResource.o");
		}

		[Test]
		public void testWriteResource()
		{
			compareResourceOEO("resource/writeResource.o");
		}

		[Test]
		public void testWriteWithResource()
		{
			compareResourceOEO("resource/writeWithResource.o");
		}

	}
}

