using NUnit.Framework;
using presto.parser;

namespace presto.translate.epe
{

	[TestFixture]
	public class TestSingleton : BaseEParserTest
	{

		[Test]
		public void testAttribute()
		{
			compareResourceEPE("singleton/attribute.e");
		}

		[Test]
		public void testMember()
		{
			compareResourceEPE("singleton/member.e");
		}

	}
}

