// generated: 2015-07-05T23:01:01.404
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

