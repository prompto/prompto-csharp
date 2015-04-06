using NUnit.Framework;
using presto.parser;

namespace presto.translate.oeo
{

	[TestFixture]
	public class TestSingleton : BaseOParserTest
	{

		[Test]
		public void testAttribute()
		{
			compareResourceOEO("singleton/attribute.poc");
		}

		[Test]
		public void testMember()
		{
			compareResourceOEO("singleton/member.poc");
		}

	}
}

