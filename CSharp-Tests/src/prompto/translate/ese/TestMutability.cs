// generated: 2015-07-05T23:01:01.348
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
		public void testMutableArgument()
		{
			compareResourceESE("mutability/mutableArgument.pec");
		}

		[Test]
		public void testMutableMember()
		{
			compareResourceESE("mutability/mutableMember.pec");
		}

	}
}

