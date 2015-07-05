// generated: 2015-07-05T23:01:01.400
using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.eoe
{

	[TestFixture]
	public class TestSingleton : BaseEParserTest
	{

		[Test]
		public void testAttribute()
		{
			compareResourceEOE("singleton/attribute.pec");
		}

		[Test]
		public void testMember()
		{
			compareResourceEOE("singleton/member.pec");
		}

	}
}

