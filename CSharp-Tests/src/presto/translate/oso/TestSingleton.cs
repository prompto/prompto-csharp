using NUnit.Framework;
using presto.parser;

namespace presto.translate.oso
{

	[TestFixture]
	public class TestSingleton : BaseOParserTest
	{

		[Test]
		public void testAttribute()
		{
			compareResourceOSO("singleton/attribute.poc");
		}

		[Test]
		public void testMember()
		{
			compareResourceOSO("singleton/member.poc");
		}

	}
}

