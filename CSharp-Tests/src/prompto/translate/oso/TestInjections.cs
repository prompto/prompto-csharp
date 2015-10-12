using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.oso
{

	[TestFixture]
	public class TestInjections : BaseOParserTest
	{

		[Test]
		public void testExpressionInjection()
		{
			compareResourceOSO("injections/expressionInjection.poc");
		}

	}
}

