using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.omo
{

	[TestFixture]
	public class TestResource : BaseOParserTest
	{

		[Test]
		public void testReadResource()
		{
			compareResourceOMO("resource/readResource.poc");
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
		public void testWriteWithResource()
		{
			compareResourceOMO("resource/writeWithResource.poc");
		}

	}
}

