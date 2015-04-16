using NUnit.Framework;
using presto.parser;

namespace presto.translate.oeo
{

	[TestFixture]
	public class TestMutability : BaseOParserTest
	{

		[Test]
		public void testImmutable()
		{
			compareResourceOEO("mutability/immutable.poc");
		}

		[Test]
		public void testMutable()
		{
			compareResourceOEO("mutability/mutable.poc");
		}

	}
}

