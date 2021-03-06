using NUnit.Framework;
using prompto.parser;
using prompto.utils;

namespace prompto.runtime.o
{

	[TestFixture]
	public class TestResource : BaseOParserTest
	{

		[SetUp]
		public void before()
		{
			Out.init();
		}

		[TearDown]
		public void after()
		{
			Out.restore();
		}

		[Test]
		public void testReadInDoWhile()
		{
			CheckOutput("resource/readInDoWhile.poc");
		}

		[Test]
		public void testReadInForEach()
		{
			CheckOutput("resource/readInForEach.poc");
		}

		[Test]
		public void testReadInIf()
		{
			CheckOutput("resource/readInIf.poc");
		}

		[Test]
		public void testReadInWhile()
		{
			CheckOutput("resource/readInWhile.poc");
		}

		[Test]
		public void testReadResource()
		{
			CheckOutput("resource/readResource.poc");
		}

		[Test]
		public void testReadResourceThen()
		{
			CheckOutput("resource/readResourceThen.poc");
		}

		[Test]
		public void testReadWithResource()
		{
			CheckOutput("resource/readWithResource.poc");
		}

		[Test]
		public void testWriteResource()
		{
			CheckOutput("resource/writeResource.poc");
		}

		[Test]
		public void testWriteResourceThen()
		{
			CheckOutput("resource/writeResourceThen.poc");
		}

		[Test]
		public void testWriteWithResource()
		{
			CheckOutput("resource/writeWithResource.poc");
		}

	}
}

