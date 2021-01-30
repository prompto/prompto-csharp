using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.omo
{

	[TestFixture]
	public class TestProblems : BaseOParserTest
	{

		[Test]
		public void testAbstract()
		{
			compareResourceOMO("problems/abstract.poc");
		}

	}
}

