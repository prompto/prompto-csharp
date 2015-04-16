using NUnit.Framework;
using presto.parser;

namespace presto.translate.eoe
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
		public void testMutable()
		{
			compareResourceEOE("mutability/mutable.pec");
		}

	}
}

