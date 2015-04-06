using NUnit.Framework;
using presto.parser;

namespace presto.translate.ese
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

