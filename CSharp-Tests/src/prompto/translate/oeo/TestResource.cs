// generated: 2015-07-05T23:01:01.382
using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.oeo
{

	[TestFixture]
	public class TestResource : BaseOParserTest
	{

		[Test]
		public void testBadRead()
		{
			compareResourceOEO("resource/badRead.poc");
		}

		[Test]
		public void testBadResource()
		{
			compareResourceOEO("resource/badResource.poc");
		}

		[Test]
		public void testBadWrite()
		{
			compareResourceOEO("resource/badWrite.poc");
		}

		[Test]
		public void testReadResource()
		{
			compareResourceOEO("resource/readResource.poc");
		}

		[Test]
		public void testReadWithResource()
		{
			compareResourceOEO("resource/readWithResource.poc");
		}

		[Test]
		public void testWriteResource()
		{
			compareResourceOEO("resource/writeResource.poc");
		}

		[Test]
		public void testWriteWithResource()
		{
			compareResourceOEO("resource/writeWithResource.poc");
		}

	}
}

