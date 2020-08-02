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
		public void testInternal()
		{
			CheckOutput("singleton/internal.poc");
		}

		[Test]
		public void testMember()
		{
			CheckOutput("singleton/member.poc");
		}

	}
}

