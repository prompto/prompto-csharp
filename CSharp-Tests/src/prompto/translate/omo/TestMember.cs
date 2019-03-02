using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.omo
{

	[TestFixture]
	public class TestMember : BaseOParserTest
	{

		[Test]
		public void testMemberAttribute()
		{
			compareResourceOMO("member/memberAttribute.poc");
		}

	}
}

