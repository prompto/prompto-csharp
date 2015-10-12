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
		public void testImmutableMember()
		{
			CheckOutput("mutability/immutableMember.pec");
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
		public void testMutableMember()
		{
			CheckOutput("mutability/mutableMember.pec");
		}

	}
}

