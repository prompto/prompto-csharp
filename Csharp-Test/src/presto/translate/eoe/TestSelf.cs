using NUnit.Framework;
using presto.parser;

namespace presto.translate.eoe
{

	[TestFixture]
	public class TestSelf : BaseEParserTest
	{

		[Test]
		public void testSelfAsParameter()
		{
			compareResourceEOE("self/selfAsParameter.e");
		}

		[Test]
		public void testSelfMember()
		{
			compareResourceEOE("self/selfMember.e");
		}

	}
}

