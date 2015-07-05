// generated: 2015-07-05T23:01:01.284
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

