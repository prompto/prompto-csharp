using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.oeo
{

	[TestFixture]
	public class TestRecursive : BaseOParserTest
	{

		[Test]
		public void testMutuallyRecursive()
		{
			compareResourceOEO("recursive/mutuallyRecursive.poc");
		}

		[Test]
		public void testRecursive()
		{
			compareResourceOEO("recursive/recursive.poc");
		}

	}
}

