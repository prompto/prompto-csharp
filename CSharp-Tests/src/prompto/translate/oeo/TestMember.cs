using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.oeo
{

	[TestFixture]
	public class TestMember : BaseOParserTest
	{

		[Test]
		public void testMemberAttribute()
		{
			compareResourceOEO("member/memberAttribute.poc");
		}

	}
}

