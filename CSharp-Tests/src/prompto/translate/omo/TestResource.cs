using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.omo
{

	[TestFixture]
	public class TestResource : BaseOParserTest
	{

		[Test]
		public void testReadInDoWhile()
		{
			compareResourceOMO("resource/readInDoWhile.poc");
		}

		[Test]
		public void testReadInForEach()
		{
			compareResourceOMO("resource/readInForEach.poc");
		}

		[Test]
		public void testReadInIf()
		{
			compareResourceOMO("resource/readInIf.poc");
		}

		[Test]
		public void testReadInWhile()
		{
			compareResourceOMO("resource/readInWhile.poc");
		}

		[Test]
		public void testReadResource()
		{
			compareResourceOMO("resource/readResource.poc");
		}

		[Test]
		public void testReadResourceThen()
		{
			compareResourceOMO("resource/readResourceThen.poc");
		}

		[Test]
		public void testReadWithResource()
		{
			compareResourceOMO("resource/readWithResource.poc");
		}

		[Test]
		public void testWriteResource()
		{
			compareResourceOMO("resource/writeResource.poc");
		}

		[Test]
		public void testWriteResourceThen()
		{
			compareResourceOMO("resource/writeResourceThen.poc");
		}

		[Test]
		public void testWriteWithResource()
		{
			compareResourceOMO("resource/writeWithResource.poc");
		}

	}
}

