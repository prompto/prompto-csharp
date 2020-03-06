using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.eme
{

	[TestFixture]
	public class TestMutability : BaseEParserTest
	{

		[Test]
		public void testImmutable()
		{
			compareResourceEME("mutability/immutable.pec");
		}

		[Test]
		public void testImmutableArgument()
		{
			compareResourceEME("mutability/immutableArgument.pec");
		}

		[Test]
		public void testImmutableDict()
		{
			compareResourceEME("mutability/immutableDict.pec");
		}

		[Test]
		public void testImmutableList()
		{
			compareResourceEME("mutability/immutableList.pec");
		}

		[Test]
		public void testImmutableMember()
		{
			compareResourceEME("mutability/immutableMember.pec");
		}

		[Test]
		public void testImmutableTuple()
		{
			compareResourceEME("mutability/immutableTuple.pec");
		}

		[Test]
		public void testMutable()
		{
			compareResourceEME("mutability/mutable.pec");
		}

		[Test]
		public void testMutableArgument()
		{
			compareResourceEME("mutability/mutableArgument.pec");
		}

		[Test]
		public void testMutableChild()
		{
			compareResourceEME("mutability/mutableChild.pec");
		}

		[Test]
		public void testMutableDict()
		{
			compareResourceEME("mutability/mutableDict.pec");
		}

		[Test]
		public void testMutableInstance()
		{
			compareResourceEME("mutability/mutableInstance.pec");
		}

		[Test]
		public void testMutableList()
		{
			compareResourceEME("mutability/mutableList.pec");
		}

		[Test]
		public void testMutableTuple()
		{
			compareResourceEME("mutability/mutableTuple.pec");
		}

	}
}

