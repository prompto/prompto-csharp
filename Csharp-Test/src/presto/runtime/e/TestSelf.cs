using NUnit.Framework;
using presto.parser;
using presto.utils;

namespace presto.runtime.e
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
			CheckOutput("self/selfAsParameter.e");
		}

		[Test]
		public void testSelfMember()
		{
			CheckOutput("self/selfMember.e");
		}

	}
}

