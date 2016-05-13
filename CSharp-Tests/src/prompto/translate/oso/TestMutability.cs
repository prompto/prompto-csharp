using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.oso
{

	[TestFixture]
	public class TestMutability : BaseOParserTest
	{

		[Test]
		public void testImmutable()
		{
			compareResourceOSO("mutability/immutable.poc");
		}

		[Test]
		public void testImmutableArgument()
		{
			compareResourceOSO("mutability/immutableArgument.poc");
		}

		[Test]
		public void testImmutableDict()
		{
			compareResourceOSO("mutability/immutableDict.poc");
		}

		[Test]
		public void testImmutableList()
		{
			compareResourceOSO("mutability/immutableList.poc");
		}

		[Test]
		public void testImmutableMember()
		{
			compareResourceOSO("mutability/immutableMember.poc");
		}

		[Test]
		public void testImmutableTuple()
		{
			compareResourceOSO("mutability/immutableTuple.poc");
		}

		[Test]
		public void testMutable()
		{
			compareResourceOSO("mutability/mutable.poc");
		}

		[Test]
		public void testMutableArgument()
		{
			compareResourceOSO("mutability/mutableArgument.poc");
		}

		[Test]
		public void testMutableDict()
		{
			compareResourceOSO("mutability/mutableDict.poc");
		}

		[Test]
		public void testMutableList()
		{
			compareResourceOSO("mutability/mutableList.poc");
		}

		[Test]
		public void testMutableMember()
		{
			compareResourceOSO("mutability/mutableMember.poc");
		}

		[Test]
		public void testMutableTuple()
		{
			compareResourceOSO("mutability/mutableTuple.poc");
		}

	}
}

