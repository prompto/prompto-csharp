using NUnit.Framework;
using prompto.parser;
using prompto.utils;

namespace prompto.runtime.o
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
		public void testImmutableArgument()
		{
			CheckOutput("mutability/immutableArgument.poc");
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
		public void testMutableArgument()
		{
			CheckOutput("mutability/mutableArgument.poc");
		}

		[Test]
		public void testMutableMember()
		{
			CheckOutput("mutability/mutableMember.poc");
		}

	}
}

