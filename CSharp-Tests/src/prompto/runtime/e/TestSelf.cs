using NUnit.Framework;
using prompto.parser;
using prompto.utils;

namespace prompto.runtime.e
{

	[TestFixture]
	public class TestSelf : BaseEParserTest
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
		public void testSelfAsParameter()
		{
			CheckOutput("self/selfAsParameter.pec");
		}

	}
}

