using NUnit.Framework;
using presto.parser;

namespace presto.translate.opo
{

	[TestFixture]
	public class TestSingleton : BaseOParserTest
	{

		[Test]
		public void testAttribute()
		{
			compareResourceOPO("singleton/attribute.o");
		}

		[Test]
		public void testMember()
		{
			compareResourceOPO("singleton/member.o");
		}

	}
}

