// generated: 2015-07-05T23:01:01.386
using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.ese
{

	[TestFixture]
	public class TestSelf : BaseEParserTest
	{

		[Test]
		public void testSelfAsParameter()
		{
			compareResourceESE("self/selfAsParameter.pec");
		}

		[Test]
		public void testSelfMember()
		{
			compareResourceESE("self/selfMember.pec");
		}

	}
}

