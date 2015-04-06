using NUnit.Framework;
using presto.parser;

namespace presto.translate.oso
{

	[TestFixture]
	public class TestResource : BaseOParserTest
	{

		[Test]
		public void testBadRead()
		{
			compareResourceOSO("resource/badRead.poc");
		}

		[Test]
		public void testBadResource()
		{
			compareResourceOSO("resource/badResource.poc");
		}

		[Test]
		public void testBadWrite()
		{
			compareResourceOSO("resource/badWrite.poc");
		}

		[Test]
		public void testReadResource()
		{
			compareResourceOSO("resource/readResource.poc");
		}

		[Test]
		public void testReadWithResource()
		{
			compareResourceOSO("resource/readWithResource.poc");
		}

		[Test]
		public void testWriteResource()
		{
			compareResourceOSO("resource/writeResource.poc");
		}

		[Test]
		public void testWriteWithResource()
		{
			compareResourceOSO("resource/writeWithResource.poc");
		}

	}
}

