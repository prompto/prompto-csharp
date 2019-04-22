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
		public void testImmutableDict()
		{
			compareResourceEOE("mutability/immutableDict.pec");
		}

		[Test]
		public void testImmutableList()
		{
			compareResourceEOE("mutability/immutableList.pec");
		}

		[Test]
		public void testImmutableMember()
		{
			compareResourceEOE("mutability/immutableMember.pec");
		}

		[Test]
		public void testImmutableTuple()
		{
			compareResourceEOE("mutability/immutableTuple.pec");
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
		public void testMutableChild()
		{
			compareResourceEOE("mutability/mutableChild.pec");
		}

		[Test]
		public void testMutableDict()
		{
			compareResourceEOE("mutability/mutableDict.pec");
		}

		[Test]
		public void testMutableInstance()
		{
			compareResourceEOE("mutability/mutableInstance.pec");
		}

		[Test]
		public void testMutableList()
		{
			compareResourceEOE("mutability/mutableList.pec");
		}

		[Test]
		public void testMutableMember()
		{
			compareResourceEOE("mutability/mutableMember.pec");
		}

		[Test]
		public void testMutableTuple()
		{
			compareResourceEOE("mutability/mutableTuple.pec");
		}

	}
}

