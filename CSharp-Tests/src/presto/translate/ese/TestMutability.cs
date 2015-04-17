using NUnit.Framework;
using presto.parser;

namespace presto.translate.ese
{

	[TestFixture]
	public class TestMutability : BaseEParserTest
	{

		[Test]
		public void testImmutable()
		{
			compareResourceESE("mutability/immutable.pec");
		}

		[Test]
		public void testImmutableMember()
		{
			compareResourceESE("mutability/immutableMember.pec");
		}

		[Test]
		public void testMutable()
		{
			compareResourceESE("mutability/mutable.pec");
		}

		[Test]
		public void testMutableMember()
		{
			compareResourceESE("mutability/mutableMember.pec");
		}

	}
}

