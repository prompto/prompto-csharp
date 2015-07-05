// generated: 2015-07-05T23:01:01.403
using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.oeo
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

