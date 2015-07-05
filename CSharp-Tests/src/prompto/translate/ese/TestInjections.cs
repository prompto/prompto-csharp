// generated: 2015-07-05T23:01:01.281
using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.ese
{

	[TestFixture]
	public class TestInjections : BaseEParserTest
	{

		[Test]
		public void testExpressionInjection()
		{
			compareResourceESE("injections/expressionInjection.pec");
		}

	}
}

