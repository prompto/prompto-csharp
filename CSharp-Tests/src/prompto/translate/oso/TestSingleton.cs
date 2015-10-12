using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.oso
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

