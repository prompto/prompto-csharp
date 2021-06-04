using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.oeo
{

	[TestFixture]
	public class TestResource : BaseOParserTest
	{

		[Test]
		public void testReadInDoWhile()
		{
			compareResourceOEO("resource/readInDoWhile.poc");
		}

		[Test]
		public void testReadInForEach()
		{
			compareResourceOEO("resource/readInForEach.poc");
		}

		[Test]
		public void testReadInIf()
		{
			compareResourceOEO("resource/readInIf.poc");
		}

		[Test]
		public void testReadInWhile()
		{
			compareResourceOEO("resource/readInWhile.poc");
		}

		[Test]
		public void testReadResource()
		{
			compareResourceOEO("resource/readResource.poc");
		}

		[Test]
		public void testReadResourceThen()
		{
			compareResourceOEO("resource/readResourceThen.poc");
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
		public void testWriteResourceThen()
		{
			compareResourceOEO("resource/writeResourceThen.poc");
		}

		[Test]
		public void testWriteWithResource()
		{
			compareResourceOEO("resource/writeWithResource.poc");
		}

	}
}

