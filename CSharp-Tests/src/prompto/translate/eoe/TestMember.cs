using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.eoe
{

	[TestFixture]
	public class TestMember : BaseEParserTest
	{

		[Test]
		public void testMemberAttribute()
		{
			compareResourceEOE("member/memberAttribute.pec");
		}

	}
}

