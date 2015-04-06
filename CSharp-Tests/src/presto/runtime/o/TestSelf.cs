using NUnit.Framework;
using presto.parser;
using presto.utils;

namespace presto.runtime.o
{

	[TestFixture]
	public class TestSelf : BaseOParserTest
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
			CheckOutput("self/selfAsParameter.poc");
		}

		[Test]
		public void testSelfMember()
		{
			CheckOutput("self/selfMember.poc");
		}

	}
}

