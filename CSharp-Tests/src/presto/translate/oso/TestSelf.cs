using NUnit.Framework;
using presto.parser;

namespace presto.translate.oso
{

	[TestFixture]
	public class TestSelf : BaseOParserTest
	{

		[Test]
		public void testSelfAsParameter()
		{
			compareResourceOSO("self/selfAsParameter.poc");
		}

		[Test]
		public void testSelfMember()
		{
			compareResourceOSO("self/selfMember.poc");
		}

	}
}

