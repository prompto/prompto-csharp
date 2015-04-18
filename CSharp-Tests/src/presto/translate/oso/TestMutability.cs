using NUnit.Framework;
using presto.parser;

namespace presto.translate.oso
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
		public void testImmutableMember()
		{
			compareResourceOSO("mutability/immutableMember.poc");
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
		public void testMutableMember()
		{
			compareResourceOSO("mutability/mutableMember.poc");
		}

	}
}

