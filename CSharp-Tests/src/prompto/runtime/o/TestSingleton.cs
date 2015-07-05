// generated: 2015-07-05T23:01:01.405
using NUnit.Framework;
using prompto.parser;
using prompto.utils;

namespace prompto.runtime.o
{

	[TestFixture]
	public class TestSingleton : BaseOParserTest
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
		public void testAttribute()
		{
			CheckOutput("singleton/attribute.poc");
		}

		[Test]
		public void testMember()
		{
			CheckOutput("singleton/member.poc");
		}

	}
}

