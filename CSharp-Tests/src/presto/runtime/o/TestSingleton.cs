using NUnit.Framework;
using presto.parser;
using presto.utils;

namespace presto.runtime.o
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

