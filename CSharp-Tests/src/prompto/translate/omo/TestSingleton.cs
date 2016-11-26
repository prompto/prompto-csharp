using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.omo
{

	[TestFixture]
	public class TestSingleton : BaseOParserTest
	{

		[Test]
		public void testAttribute()
		{
			compareResourceOMO("singleton/attribute.poc");
		}

		[Test]
		public void testMember()
		{
			compareResourceOMO("singleton/member.poc");
		}

	}
}
