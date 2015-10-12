using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.eoe
{

	[TestFixture]
	public class TestSelf : BaseEParserTest
	{

		[Test]
		public void testSelfAsParameter()
		{
			compareResourceEOE("self/selfAsParameter.pec");
		}

		[Test]
		public void testSelfMember()
		{
			compareResourceEOE("self/selfMember.pec");
		}

	}
}

