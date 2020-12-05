using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.oeo
{

	[TestFixture]
	public class TestSingleton : BaseOParserTest
	{

		[Test]
		public void testAttribute()
		{
			compareResourceOEO("singleton/attribute.poc");
		}

		[Test]
		public void testInitialize()
		{
			compareResourceOEO("singleton/initialize.poc");
		}

		[Test]
		public void testInternal()
		{
			compareResourceOEO("singleton/internal.poc");
		}

		[Test]
		public void testMember()
		{
			compareResourceOEO("singleton/member.poc");
		}

	}
}

