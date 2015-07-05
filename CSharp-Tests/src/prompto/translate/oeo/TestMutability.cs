// generated: 2015-07-05T23:01:01.350
using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.oeo
{

	[TestFixture]
	public class TestMutability : BaseOParserTest
	{

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
		public void testImmutableMember()
		{
			compareResourceOEO("mutability/immutableMember.poc");
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
		public void testMutableMember()
		{
			compareResourceOEO("mutability/mutableMember.poc");
		}

	}
}

