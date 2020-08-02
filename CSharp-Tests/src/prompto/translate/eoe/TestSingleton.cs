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
		public void testInternal()
		{
			compareResourceEOE("singleton/internal.pec");
		}

		[Test]
		public void testMember()
		{
			compareResourceEOE("singleton/member.pec");
		}

	}
}

