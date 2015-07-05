// generated: 2015-07-05T23:01:01.391
using NUnit.Framework;
using prompto.parser;
using prompto.utils;

namespace prompto.runtime.o
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

