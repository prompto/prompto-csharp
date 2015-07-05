// generated: 2015-07-05T23:01:01.388
using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.oeo
{

	[TestFixture]
	public class TestSelf : BaseOParserTest
	{

		[Test]
		public void testSelfAsParameter()
		{
			compareResourceOEO("self/selfAsParameter.poc");
		}

		[Test]
		public void testSelfMember()
		{
			compareResourceOEO("self/selfMember.poc");
		}

	}
}

