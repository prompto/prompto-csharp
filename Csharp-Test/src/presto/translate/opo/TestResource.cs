using NUnit.Framework;
using presto.parser;

namespace presto.translate.opo
{

	[TestFixture]
	public class TestResource : BaseOParserTest
	{

		[Test]
		public void testBadRead()
		{
			compareResourceOPO("resource/badRead.o");
		}

		[Test]
		public void testBadResource()
		{
			compareResourceOPO("resource/badResource.o");
		}

		[Test]
		public void testBadWrite()
		{
			compareResourceOPO("resource/badWrite.o");
		}

		[Test]
		public void testReadResource()
		{
			compareResourceOPO("resource/readResource.o");
		}

		[Test]
		public void testReadWithResource()
		{
			compareResourceOPO("resource/readWithResource.o");
		}

		[Test]
		public void testWriteResource()
		{
			compareResourceOPO("resource/writeResource.o");
		}

		[Test]
		public void testWriteWithResource()
		{
			compareResourceOPO("resource/writeWithResource.o");
		}

	}
}

