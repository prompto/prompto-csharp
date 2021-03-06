using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.oeo
{

	[TestFixture]
	public class TestMutability : BaseOParserTest
	{

		[Test]
		public void testDowncastMutable()
		{
			compareResourceOEO("mutability/downcastMutable.poc");
		}

		[Test]
		public void testImmutable()
		{
			compareResourceOEO("mutability/immutable.poc");
		}

		[Test]
		public void testImmutableArgument()
		{
			compareResourceOEO("mutability/immutableArgument.poc");
		}

		[Test]
		public void testImmutableDict()
		{
			compareResourceOEO("mutability/immutableDict.poc");
		}

		[Test]
		public void testImmutableList()
		{
			compareResourceOEO("mutability/immutableList.poc");
		}

		[Test]
		public void testImmutableMember()
		{
			compareResourceOEO("mutability/immutableMember.poc");
		}

		[Test]
		public void testImmutableTuple()
		{
			compareResourceOEO("mutability/immutableTuple.poc");
		}

		[Test]
		public void testMutable()
		{
			compareResourceOEO("mutability/mutable.poc");
		}

		[Test]
		public void testMutableArgument()
		{
			compareResourceOEO("mutability/mutableArgument.poc");
		}

		[Test]
		public void testMutableDict()
		{
			compareResourceOEO("mutability/mutableDict.poc");
		}

		[Test]
		public void testMutableList()
		{
			compareResourceOEO("mutability/mutableList.poc");
		}

		[Test]
		public void testMutableTuple()
		{
			compareResourceOEO("mutability/mutableTuple.poc");
		}

	}
}

