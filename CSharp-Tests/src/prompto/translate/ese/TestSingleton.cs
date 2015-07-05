// generated: 2015-07-05T23:01:01.401
using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.ese
{

	[TestFixture]
	public class TestSingleton : BaseEParserTest
	{

		[Test]
		public void testAttribute()
		{
			compareResourceESE("singleton/attribute.pec");
		}

		[Test]
		public void testMember()
		{
			compareResourceESE("singleton/member.pec");
		}

	}
}

