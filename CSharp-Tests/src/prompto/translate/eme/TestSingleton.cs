using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.eme
{

	[TestFixture]
	public class TestSingleton : BaseEParserTest
	{

		[Test]
		public void testAttribute()
		{
			compareResourceEME("singleton/attribute.pec");
		}

		[Test]
		public void testInternal()
		{
			compareResourceEME("singleton/internal.pec");
		}

		[Test]
		public void testMember()
		{
			compareResourceEME("singleton/member.pec");
		}

	}
}

