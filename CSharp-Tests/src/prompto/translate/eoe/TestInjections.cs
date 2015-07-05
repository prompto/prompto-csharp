// generated: 2015-07-05T23:01:01.279
using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.eoe
{

	[TestFixture]
	public class TestInjections : BaseEParserTest
	{

		[Test]
		public void testExpressionInjection()
		{
			compareResourceEOE("injections/expressionInjection.pec");
		}

	}
}

