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

