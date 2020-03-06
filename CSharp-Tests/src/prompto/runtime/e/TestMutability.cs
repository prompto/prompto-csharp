using NUnit.Framework;
using prompto.parser;
using prompto.utils;

namespace prompto.runtime.e
{

	[TestFixture]
	public class TestMutability : BaseEParserTest
	{

		[SetUp]
		public void before()
		{
			Out.init();
		}

		[TearDown]
		public void after()
		{
			Out.restore();
		}

		[Test]
		public void testImmutable()
		{
			CheckOutput("mutability/immutable.pec");
		}

		[Test]
		public void testImmutableArgument()
		{
			CheckOutput("mutability/immutableArgument.pec");
		}

		[Test]
		public void testImmutableDict()
		{
			CheckOutput("mutability/immutableDict.pec");
		}

		[Test]
		public void testImmutableList()
		{
			CheckOutput("mutability/immutableList.pec");
		}

		[Test]
		public void testImmutableMember()
		{
			CheckOutput("mutability/immutableMember.pec");
		}

		[Test]
		public void testImmutableTuple()
		{
			CheckOutput("mutability/immutableTuple.pec");
		}

		[Test]
		public void testMutable()
		{
			CheckOutput("mutability/mutable.pec");
		}

		[Test]
		public void testMutableArgument()
		{
			CheckOutput("mutability/mutableArgument.pec");
		}

		[Test]
		public void testMutableChild()
		{
			CheckOutput("mutability/mutableChild.pec");
		}

		[Test]
		public void testMutableDict()
		{
			CheckOutput("mutability/mutableDict.pec");
		}

		[Test]
		public void testMutableInstance()
		{
			CheckOutput("mutability/mutableInstance.pec");
		}

		[Test]
		public void testMutableList()
		{
			CheckOutput("mutability/mutableList.pec");
		}

		[Test]
		public void testMutableTuple()
		{
			CheckOutput("mutability/mutableTuple.pec");
		}

	}
}

