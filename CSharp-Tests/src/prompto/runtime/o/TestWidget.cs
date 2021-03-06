using NUnit.Framework;
using prompto.parser;
using prompto.utils;

namespace prompto.runtime.o
{

	[TestFixture]
	public class TestWidget : BaseOParserTest
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
		public void testMinimal()
		{
			CheckOutput("widget/minimal.poc");
		}

		[Test]
		public void testNative()
		{
			CheckOutput("widget/native.poc");
		}

		[Test]
		public void testWithEvent()
		{
			CheckOutput("widget/withEvent.poc");
		}

	}
}

