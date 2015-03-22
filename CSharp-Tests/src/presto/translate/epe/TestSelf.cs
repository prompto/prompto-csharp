using NUnit.Framework;
using presto.parser;

namespace presto.translate.epe
{

	[TestFixture]
	public class TestSelf : BaseEParserTest
	{

		[Test]
		public void testSelfAsParameter()
		{
			compareResourceEPE("self/selfAsParameter.e");
		}

		[Test]
		public void testSelfMember()
		{
			compareResourceEPE("self/selfMember.e");
		}

	}
}

