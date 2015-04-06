using NUnit.Framework;
using presto.parser;

namespace presto.translate.oso
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

