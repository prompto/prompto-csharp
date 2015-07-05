// generated: 2015-07-05T23:01:01.402
using NUnit.Framework;
using prompto.parser;
using prompto.utils;

namespace prompto.runtime.e
{

	[TestFixture]
	public class TestSingleton : BaseEParserTest
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
			CheckOutput("singleton/attribute.pec");
		}

		[Test]
		public void testMember()
		{
			CheckOutput("singleton/member.pec");
		}

	}
}

