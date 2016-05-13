using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.ese
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
		public void testImmutableArgument()
		{
			compareResourceESE("mutability/immutableArgument.pec");
		}

		[Test]
		public void testImmutableDict()
		{
			compareResourceESE("mutability/immutableDict.pec");
		}

		[Test]
		public void testImmutableList()
		{
			compareResourceESE("mutability/immutableList.pec");
		}

		[Test]
		public void testImmutableMember()
		{
			compareResourceESE("mutability/immutableMember.pec");
		}

		[Test]
		public void testImmutableTuple()
		{
			compareResourceESE("mutability/immutableTuple.pec");
		}

		[Test]
		public void testMutable()
		{
			compareResourceESE("mutability/mutable.pec");
		}

		[Test]
		public void testMutableArgument()
		{
			compareResourceESE("mutability/mutableArgument.pec");
		}

		[Test]
		public void testMutableDict()
		{
			compareResourceESE("mutability/mutableDict.pec");
		}

		[Test]
		public void testMutableList()
		{
			compareResourceESE("mutability/mutableList.pec");
		}

		[Test]
		public void testMutableMember()
		{
			compareResourceESE("mutability/mutableMember.pec");
		}

		[Test]
		public void testMutableTuple()
		{
			compareResourceESE("mutability/mutableTuple.pec");
		}

	}
}

