using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.omo
{

	[TestFixture]
	public class TestRecursive : BaseOParserTest
	{

		[Test]
		public void testMutuallyRecursive()
		{
			compareResourceOMO("recursive/mutuallyRecursive.poc");
		}

		[Test]
		public void testRecursive()
		{
			compareResourceOMO("recursive/recursive.poc");
		}

	}
}

