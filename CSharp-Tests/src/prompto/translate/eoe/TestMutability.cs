// generated: 2015-07-05T23:01:01.347
using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.eoe
{

	[TestFixture]
	public class TestMutability : BaseEParserTest
	{

		[Test]
		public void testImmutable()
		{
			compareResourceEOE("mutability/immutable.pec");
		}

		[Test]
		public void testImmutableArgument()
		{
			compareResourceEOE("mutability/immutableArgument.pec");
		}

		[Test]
		public void testImmutableMember()
		{
			compareResourceEOE("mutability/immutableMember.pec");
		}

		[Test]
		public void testMutable()
		{
			compareResourceEOE("mutability/mutable.pec");
		}

		[Test]
		public void testMutableArgument()
		{
			compareResourceEOE("mutability/mutableArgument.pec");
		}

		[Test]
		public void testMutableMember()
		{
			compareResourceEOE("mutability/mutableMember.pec");
		}

	}
}

