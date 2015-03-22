using NUnit.Framework;
using presto.parser;

namespace presto.translate.oeo
{

	[TestFixture]
	public class TestSelf : BaseOParserTest
	{

		[Test]
		public void testSelfAsParameter()
		{
			compareResourceOEO("self/selfAsParameter.o");
		}

		[Test]
		public void testSelfMember()
		{
			compareResourceOEO("self/selfMember.o");
		}

	}
}

