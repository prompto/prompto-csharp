using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.eme
{

	[TestFixture]
	public class TestDebug : BaseEParserTest
	{

		[Test]
		public void testStack()
		{
			compareResourceEME("debug/stack.pec");
		}

		[Test]
		public void testVariables()
		{
			compareResourceEME("debug/variables.pec");
		}

	}
}

