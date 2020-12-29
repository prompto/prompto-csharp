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
		public void testDowncastMutable()
		{
			CheckOutput("mutability/downcastMutable.poc");
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
		public void testImmutableDict()
		{
			CheckOutput("mutability/immutableDict.poc");
		}

		[Test]
		public void testImmutableList()
		{
			CheckOutput("mutability/immutableList.poc");
		}

		[Test]
		public void testImmutableMember()
		{
			CheckOutput("mutability/immutableMember.poc");
		}

		[Test]
		public void testImmutableTuple()
		{
			CheckOutput("mutability/immutableTuple.poc");
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
		public void testMutableDict()
		{
			CheckOutput("mutability/mutableDict.poc");
		}

		[Test]
		public void testMutableList()
		{
			CheckOutput("mutability/mutableList.poc");
		}

		[Test]
		public void testMutableTuple()
		{
			CheckOutput("mutability/mutableTuple.poc");
		}

	}
}

