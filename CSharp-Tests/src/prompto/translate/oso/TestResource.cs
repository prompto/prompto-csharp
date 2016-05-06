using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.oso
{

	[TestFixture]
	public class TestResource : BaseOParserTest
	{

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

