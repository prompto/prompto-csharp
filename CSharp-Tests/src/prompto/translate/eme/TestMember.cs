using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.eme
{

	[TestFixture]
	public class TestMember : BaseEParserTest
	{

		[Test]
		public void testMemberAttribute()
		{
			compareResourceEME("member/memberAttribute.pec");
		}

	}
}

