using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.omo
{

	[TestFixture]
	public class TestMutability : BaseOParserTest
	{

		[Test]
		public void testImmutable()
		{
			compareResourceOMO("mutability/immutable.poc");
		}

		[Test]
		public void testImmutableArgument()
		{
			compareResourceOMO("mutability/immutableArgument.poc");
		}

		[Test]
		public void testImmutableDict()
		{
			compareResourceOMO("mutability/immutableDict.poc");
		}

		[Test]
		public void testImmutableList()
		{
			compareResourceOMO("mutability/immutableList.poc");
		}

		[Test]
		public void testImmutableMember()
		{
			compareResourceOMO("mutability/immutableMember.poc");
		}

		[Test]
		public void testImmutableTuple()
		{
			compareResourceOMO("mutability/immutableTuple.poc");
		}

		[Test]
		public void testMutable()
		{
			compareResourceOMO("mutability/mutable.poc");
		}

		[Test]
		public void testMutableArgument()
		{
			compareResourceOMO("mutability/mutableArgument.poc");
		}

		[Test]
		public void testMutableDict()
		{
			compareResourceOMO("mutability/mutableDict.poc");
		}

		[Test]
		public void testMutableList()
		{
			compareResourceOMO("mutability/mutableList.poc");
		}

		[Test]
		public void testMutableTuple()
		{
			compareResourceOMO("mutability/mutableTuple.poc");
		}

	}
}

