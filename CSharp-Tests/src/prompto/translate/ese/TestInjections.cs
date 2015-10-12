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

