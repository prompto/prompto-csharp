using NUnit.Framework;
using presto.parser;
using presto.utils;

namespace presto.runtime.e
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
		public void testMutable()
		{
			CheckOutput("mutability/mutable.pec");
		}

	}
}

