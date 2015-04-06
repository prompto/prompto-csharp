using NUnit.Framework;
using presto.parser;
using presto.utils;

namespace presto.runtime.e
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

