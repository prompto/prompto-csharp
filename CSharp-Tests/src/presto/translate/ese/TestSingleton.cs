using NUnit.Framework;
using presto.parser;

namespace presto.translate.ese
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

