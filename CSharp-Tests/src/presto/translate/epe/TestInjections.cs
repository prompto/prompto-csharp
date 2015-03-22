using NUnit.Framework;
using presto.parser;

namespace presto.translate.epe
{

	[TestFixture]
	public class TestInjections : BaseEParserTest
	{

		[Test]
		public void testExpressionInjection()
		{
			compareResourceEPE("injections/expressionInjection.e");
		}

	}
}

