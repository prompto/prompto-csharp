using NUnit.Framework;
using presto.parser;

namespace presto.translate.eoe
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

