using NUnit.Framework;
using presto.parser;
using presto.utils;

namespace presto.runtime.o
{

	[TestFixture]
	public class TestMutability : BaseOParserTest
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
		public void testImmutable()
		{
			CheckOutput("mutability/immutable.poc");
		}

		[Test]
		public void testImmutableMember()
		{
			CheckOutput("mutability/immutableMember.poc");
		}

		[Test]
		public void testMutable()
		{
			CheckOutput("mutability/mutable.poc");
		}

		[Test]
		public void testMutableMember()
		{
			CheckOutput("mutability/mutableMember.poc");
		}

	}
}

