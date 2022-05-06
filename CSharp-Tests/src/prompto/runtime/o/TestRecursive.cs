using NUnit.Framework;
using prompto.parser;
using prompto.utils;

namespace prompto.runtime.o
{

	[TestFixture]
	public class TestRecursive : BaseOParserTest
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
		public void testMutuallyRecursive()
		{
			CheckOutput("recursive/mutuallyRecursive.poc");
		}

		[Test]
		public void testRecursive()
		{
			CheckOutput("recursive/recursive.poc");
		}

	}
}

