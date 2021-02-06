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

	}
}

