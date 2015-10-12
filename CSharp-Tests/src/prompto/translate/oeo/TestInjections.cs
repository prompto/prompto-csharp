using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.oeo
{

	[TestFixture]
	public class TestInjections : BaseOParserTest
	{

		[Test]
		public void testExpressionInjection()
		{
			compareResourceOEO("injections/expressionInjection.poc");
		}

	}
}

