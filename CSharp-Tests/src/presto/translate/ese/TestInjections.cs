using NUnit.Framework;
using presto.parser;

namespace presto.translate.ese
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

