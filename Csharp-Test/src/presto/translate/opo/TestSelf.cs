using NUnit.Framework;
using presto.parser;

namespace presto.translate.opo
{

	[TestFixture]
	public class TestSelf : BaseOParserTest
	{

		[Test]
		public void testSelfAsParameter()
		{
			compareResourceOPO("self/selfAsParameter.o");
		}

		[Test]
		public void testSelfMember()
		{
			compareResourceOPO("self/selfMember.o");
		}

	}
}

