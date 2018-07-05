using NUnit.Framework;
using prompto.parser;
using prompto.utils;

namespace prompto.runtime.e
{

	[TestFixture]
	public class TestWidget : BaseEParserTest
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
			CheckOutput("widget/minimal.pec");
		}

		[Test]
		public void testNative()
		{
			CheckOutput("widget/native.pec");
		}

	}
}

